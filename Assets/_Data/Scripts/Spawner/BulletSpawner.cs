using UnityEngine;

public class BulletSpawner : Spawner
{
    public static BulletSpawner Instance { get; private set; }

    public static int BulletPrefabIndex { get; private set; } = 1;
    // Chỉ số của prefab đạn trong danh sách prefab

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of BulletSpawner detected!");
            Destroy(gameObject);
            return;
        }
        BulletSpawner.Instance = this;
    }
}
