using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : GilMonoBehaviour
{
    [SerializeField] protected List<Transform> _prefabs;

    protected override void LoadComponents()
    {
        this.LoadPrefabs();
    }

    // Chỉ chạy trong editor
    protected virtual void LoadPrefabs()
    {
        if (this._prefabs != null && this._prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform child in prefabObj)
        {
            this._prefabs.Add(child);
        }
        this.HidePrefabs();

        Debug.Log(transform.name + " loaded prefabs: " + this._prefabs.Count);
    }
    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this._prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform SpawnPrefab(int index, Vector3 position, Quaternion rotation)
    {
        if (index < 0 || index >= this._prefabs.Count) 
        {
            Debug.LogError("Prefab index out of range: " + index);
            return null;
        }

        Transform prefab = this._prefabs[index];
        Transform instance = Instantiate(prefab, position, rotation);
        instance.gameObject.SetActive(true);
        return instance;
    }
}
