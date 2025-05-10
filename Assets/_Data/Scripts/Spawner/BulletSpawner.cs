using UnityEngine;

public class BulletSpawner : Spawner
{
    public static BulletSpawner Instance { get; private set; }

    public static int BulletPrefabIndex { get; private set; } = 1;
    // Chỉ số của prefab đạn trong danh sách prefab

    protected override void Awake()
    {
        base.Awake();
        BulletSpawner.Instance = this;
    }
}
