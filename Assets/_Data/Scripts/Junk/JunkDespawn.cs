using UnityEngine;

public class JunkDespawn : DespawnByDistance
{
    protected override void DespawnObject()
    {
        JunkSpawner.Instance.Despawn(transform.parent);
    }
}

