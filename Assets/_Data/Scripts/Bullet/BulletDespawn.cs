using UnityEngine;

public class BulletDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
}
