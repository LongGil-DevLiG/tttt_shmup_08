using UnityEngine;

public class OrbitalBuletDespawn : DespawnByTime
{
    protected override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
}
