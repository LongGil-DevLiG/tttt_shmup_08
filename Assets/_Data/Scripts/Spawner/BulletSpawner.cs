using UnityEngine;

public class BulletSpawner : Spawner
{
    public static BulletSpawner Instance { get; private set; }

    public static int BulletPrefabIndex { get; private set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        BulletSpawner.Instance = this;
    }
}
