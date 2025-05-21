using UnityEngine;

public class EnemySpawner : Spawner
{
    public static EnemySpawner Instance { get; private set; }

    // public static int EnemyPrefabIndex { get; private set; } = 0;
    // Chỉ số của prefab kẻ địch trong danh sách prefab

    public static int EnemyPrefabIndex
    {
        get
        {
            if (Instance == null) return 0;
            return Instance.GetRandomEnemyPrefabIndex();
        }
    }

    private int GetRandomEnemyPrefabIndex()
    {
        if (_prefabs == null || _prefabs.Count == 0) return 0;
        return Random.Range(0, _prefabs.Count);
    }

    // Chỉ số của prefab kẻ địch trong danh sách prefab
    public int GetPrefabIndex(Transform prefab)
    {
        if (_prefabs == null) return -1;
        for (int i = 0; i < _prefabs.Count; i++)
        {
            if (_prefabs[i] != null && _prefabs[i].name == prefab.name)
            {
                return i;
            }
        }
        return -1;
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of EnemySpawner detected!");
            Destroy(gameObject);
            return;
        }
        EnemySpawner.Instance = this;
    }
}
