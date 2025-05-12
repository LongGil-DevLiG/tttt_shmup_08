using UnityEngine;

public class JunkDespawn : DespawnByTime
{
    protected override void DespawnObject()
    {
        JunkSpawner.Instance.Despawn(transform.parent);
    }
}

