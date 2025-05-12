using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : GilMonoBehaviour
{
    [SerializeField] protected Transform holder;
    // Đối tượng cha để chứa các prefab được spawn
    [SerializeField] protected List<Transform> _prefabs;
    [SerializeField] protected List<Transform> poolObject;

    protected override void LoadComponents()
    {
        this.LoadPrefabs();
        this.LoadHolder();
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
    
    // Chỉ chạy trong editor Load Holder và tạo nếu không có
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;

        Transform holderObj = transform.Find("Holder");
        Debug.Log("Holder Loaded: " + holderObj);
        if (holderObj == null)
        {
            holderObj = new GameObject("Holder").transform;
            holderObj.SetParent(transform);
            Debug.Log("Holder created: " + this.holder);
        }
        this.holder = holderObj;
    }
    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this._prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual void Despawn(Transform obj)
    {
        // if (obj == null) return;

        // // Tìm prefab trong danh sách prefab
        // Transform prefab = this._prefabs.Find(p => p.name == obj.name);
        // if (prefab != null)
        // {
        //     obj.gameObject.SetActive(false);
        //     obj.SetParent(prefab);
        // }
        // else
        // {
        //     Debug.LogError("Prefab not found: " + obj.name);
        // }

        this.poolObject.Add(obj);
        obj.gameObject.SetActive(false);
    }
    

    public virtual Transform SpawnPrefab(int index, Vector3 position, Quaternion rotation)
    {
        if (index < 0 || index >= this._prefabs.Count) 
        {
            Debug.LogError("Prefab index out of range: " + index);
            return null;
        }

        Transform prefab = this._prefabs[index];

        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(position, rotation);

        newPrefab.parent = this.holder;
        return newPrefab;
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform obj in this.poolObject)
        {
            if (obj.name == prefab.name)
            {
                this.poolObject.Remove(obj);
                return obj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
}
