using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject[] enemyPrefabs;
        public Transform[] spawnPoints;
        public int enemyCount;
        public float spawnRate;
        public int waveScore; // Điểm thưởng khi hoàn thành wave

        
    }

    [Header("Wave Settings")]
    [SerializeField] private Wave[] waves;

    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("UI References")]
    [SerializeField] private TMPro.TextMeshProUGUI waveText;
    [SerializeField] private GameObject waveCompleteUI;

    private int currentWaveIndex = 0;
    private int enemiesRemaining = 0;
    private bool isSpawning = false;
    private bool waveCompleted = false;

    private void Start()
    {
        if (waves.Length == 0)
        {
            Debug.LogWarning("WaveManager: No waves configured.");
            return;
        }

        foreach (Wave wave in waves)
        {
            if (wave.spawnPoints == null || wave.spawnPoints.Length == 0)
            {
                Debug.LogError($"WaveManager: No spawn points assigned for wave {wave.waveName}");
                return;
            }
        }

        StartCoroutine(StartNextWave());
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;
        
        // Debug log để kiểm tra số lượng kẻ địch còn lại
        Debug.Log($"Enemy destroyed. Remaining: {enemiesRemaining}. IsSpawning: {isSpawning}");
        
        if (enemiesRemaining <= 0 && !isSpawning && !waveCompleted)
        {
            waveCompleted = true;
            CompleteWave();
        }
    }

    private void CompleteWave()
    {
        Debug.Log($"Completing wave {currentWaveIndex}");
        
        // Add wave bonus score
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(waves[currentWaveIndex].waveScore);
        }

        // Show wave complete UI
        if (waveCompleteUI != null)
        {
            waveCompleteUI.SetActive(true);
            StartCoroutine(HideWaveCompleteUI());
        }

        currentWaveIndex++;

        // Check if all waves are completed
        if (currentWaveIndex >= waves.Length)
        {
            // Game complete
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameComplete();
            }
            return;
        }

        StartCoroutine(StartNextWave());
    }

    private IEnumerator HideWaveCompleteUI()
    {
        yield return new WaitForSeconds(3f);
        if (waveCompleteUI != null)
        {
            waveCompleteUI.SetActive(false);
        }
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        
        if (currentWaveIndex < waves.Length)
        {
            waveCompleted = false;
            
            if (waveText != null)
            {
                waveText.text = "WAVE " + (currentWaveIndex + 1);
            }
            
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        if (currentWave.enemyPrefabs.Length == 0)
        {
            Debug.LogError("WaveManager: No enemy prefabs in wave " + currentWave.waveName);
            yield break;
        }

        isSpawning = true;
        enemiesRemaining = currentWave.enemyCount;

        Debug.Log($"Starting wave {currentWaveIndex}. Enemies to spawn: {enemiesRemaining}");

        // Wait one frame to ensure correct enemy count before any are destroyed
        yield return null;

        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            GameObject enemyPrefab = currentWave.enemyPrefabs[Random.Range(0, currentWave.enemyPrefabs.Length)];
            Transform spawnPoint = currentWave.spawnPoints[Random.Range(0, currentWave.spawnPoints.Length)];

        // Tìm index prefab trong danh sách EnemySpawner
        int prefabIndex = EnemySpawner.Instance.GetPrefabIndex(enemyPrefab.transform);

        if (prefabIndex != -1)
        {
            // Nếu có trong danh sách, gọi SpawnPrefab bằng index
            Transform obj = EnemySpawner.Instance.SpawnPrefab(prefabIndex, spawnPoint.position, Quaternion.identity);
            obj.gameObject.SetActive(true);
            }
            else
            {
                // Nếu không, dùng Instantiate bình thường
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(currentWave.spawnRate);
        }

        isSpawning = false;

        Debug.Log($"Wave {currentWaveIndex} spawning completed. Enemies remaining: {enemiesRemaining}");

        // Check again in case all enemies were destroyed during spawn (e.g., with homing missiles)
        // if (enemiesRemaining <= 0 && !waveCompleted)
        // {
        //     waveCompleted = true;
        //     CompleteWave();
        // }
        if (isSpawning == false)
        {
            waveCompleted = true;
            CompleteWave();
        }
    }
}