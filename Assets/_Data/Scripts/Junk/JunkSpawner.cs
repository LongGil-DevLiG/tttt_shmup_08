using UnityEngine;

public class JunkSpawner : Spawner
{
    public static JunkSpawner Instance { get; private set; }

    public static int JunkPrefabIndex { get; private set; } = 0;
    // Chỉ số của prefab rác trong danh sách prefab

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
