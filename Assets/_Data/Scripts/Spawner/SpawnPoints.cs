using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnPoints : GilMonoBehaviour
{
    [SerializeField] protected List<Transform> spawnPoints;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoints();
    }

    protected virtual void LoadPoints()
    {
        if (this.spawnPoints.Count > 0 && this.spawnPoints != null) return;

        foreach (Transform point in transform)
        {
            this.spawnPoints.Add(point);
        }
        Debug.Log($"SpawnPoints: {this.spawnPoints.Count} spawn points loaded.");
        // if (this.spawnPoints == null || this.spawnPoints.Count == 0)
        // {
        //     this.spawnPoints = new List<Transform>();
        //     Transform[] transforms = this.GetComponentsInChildren<Transform>(true);
        //     foreach (Transform transform in transforms)
        //     {
        //         if (transform != this.transform)
        //         {
        //             this.spawnPoints.Add(transform);
        //         }
        //     }
        // }
    }

    public virtual Transform GetRandom()
    {
        if (this.spawnPoints == null || this.spawnPoints.Count == 0) return null;

        int randomIndex = Random.Range(0, this.spawnPoints.Count);
        return this.spawnPoints[randomIndex];
    }
}
