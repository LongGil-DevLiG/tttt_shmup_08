using UnityEngine;

public class OrbitalBulletDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
}
