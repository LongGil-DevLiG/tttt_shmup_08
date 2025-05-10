using UnityEngine;

public class BulletDespawn : DespawnByDistance
{
    protected override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
}
