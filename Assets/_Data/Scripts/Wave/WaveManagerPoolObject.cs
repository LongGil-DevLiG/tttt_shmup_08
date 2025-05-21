using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject[] enemyPrefabs;
        public int enemyCount;
        public float spawnRate;
        public int waveScore; // Điểm thưởng khi hoàn thành wave
    }

    [Header("Wave Settings")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("UI References")]
    [SerializeField] private TMPro.TextMeshProUGUI waveText;
    [SerializeField] private GameObject waveCompleteUI;

    [Header("Object Pooling")]
    [SerializeField] private bool useObjectPooling = true;
    [SerializeField] private int initialPoolSize = 20;
    
    private int currentWaveIndex = 0;
    private int enemiesRemaining = 0;
    private bool isSpawning = false;
    private bool waveCompleted = false;
    private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        if (waves.Length == 0)
        {
            Debug.LogWarning("WaveManager: No waves configured.");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("WaveManager: No spawn points assigned.");
            return;
        }

        if (useObjectPooling)
        {
            InitializeObjectPools();
        }

        StartCoroutine(StartNextWave());
    }

    private void InitializeObjectPools()
    {
        // Lấy tất cả các prefab từ tất cả các waves
        HashSet<GameObject> allPrefabs = new HashSet<GameObject>();
        foreach (Wave wave in waves)
        {
            foreach (GameObject prefab in wave.enemyPrefabs)
            {
                if (prefab != null)
                {
                    allPrefabs.Add(prefab);
                }
            }
        }

        // Tạo pool cho mỗi prefab
        foreach (GameObject prefab in allPrefabs)
        {
            string prefabID = prefab.name;
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            objectPools[prefabID] = objectPool;
            Debug.Log($"Created pool for {prefabID} with {initialPoolSize} instances");
        }
    }

    public GameObject GetPooledObject(GameObject prefab)
    {
        string prefabID = prefab.name;
        
        // Nếu không có pool cho prefab này, tạo mới
        if (!objectPools.ContainsKey(prefabID))
        {
            objectPools[prefabID] = new Queue<GameObject>();
        }

        // Nếu pool rỗng, thêm đối tượng mới
        if (objectPools[prefabID].Count == 0)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(false);
            objectPools[prefabID].Enqueue(newObj);
            Debug.Log($"Added new object to pool for {prefabID}");
        }

        // Lấy đối tượng từ pool
        GameObject obj = objectPools[prefabID].Dequeue();
        obj.SetActive(true);
        
        // Thêm vào danh sách active
        activeEnemies.Add(obj);
        
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        string prefabName = obj.name.Replace("(Clone)", "").Trim();
        
        if (objectPools.ContainsKey(prefabName))
        {
            obj.SetActive(false);
            objectPools[prefabName].Enqueue(obj);
            
            // Xóa khỏi danh sách active
            if (activeEnemies.Contains(obj))
            {
                activeEnemies.Remove(obj);
            }
        }
        else
        {
            Debug.LogWarning($"No pool found for object '{prefabName}'. Creating new pool.");
            objectPools[prefabName] = new Queue<GameObject>();
            obj.SetActive(false);
            objectPools[prefabName].Enqueue(obj);
        }
    }

    public void EnemyDeactivated()
    {
        enemiesRemaining--;
        
        // Debug log để kiểm tra số lượng kẻ địch còn lại
        Debug.Log($"Enemy deactivated. Remaining: {enemiesRemaining}. IsSpawning: {isSpawning}");
        
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
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            if (enemyPrefab != null && spawnPoint != null)
            {
                GameObject enemy;
                
                if (useObjectPooling)
                {
                    enemy = GetPooledObject(enemyPrefab);
                    enemy.transform.position = spawnPoint.position;
                    enemy.transform.rotation = Quaternion.identity;
                    
                    // Nếu enemy có component EnemyController, reset nó
                    DamageReceiver enemyController = enemy.GetComponentInChildren<DamageReceiver>();
                    if (enemyController != null)
                    {
                        // enemyController.ResetEnemy();
                    }
                }
                else
                {
                    enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(currentWave.spawnRate);
        }

        isSpawning = false;
        
        Debug.Log($"Wave {currentWaveIndex} spawning completed. Enemies remaining: {enemiesRemaining}");

        // Check again in case all enemies were returned to pool during spawn
        if (enemiesRemaining <= 0 && !waveCompleted)
        {
            waveCompleted = true;
            CompleteWave();
        }
    }
}