using UnityEngine;

public class JunkDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        JunkSpawner.Instance.Despawn(transform.parent);
    }
}

