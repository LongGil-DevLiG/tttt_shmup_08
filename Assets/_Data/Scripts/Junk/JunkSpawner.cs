using UnityEngine;

public class JunkSpawner : Spawner
{
    public static JunkSpawner Instance { get; private set; }

    // public static int JunkPrefabIndex { get; private set; } = 0;
    // Chỉ số của prefab rác trong danh sách prefab

    public static int JunkPrefabIndex
    {
        get
        {
            if (Instance == null) return 0;
            return Instance.GetRandomJunkPrefabIndex();
        }
    }

    private int GetRandomJunkPrefabIndex()
    {
        if (_prefabs == null || _prefabs.Count == 0) return 0;
        return Random.Range(0, _prefabs.Count);
    }
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of JunkSpawner detected!");
            Destroy(gameObject);
            return;
        }
        JunkSpawner.Instance = this;
    }
}
