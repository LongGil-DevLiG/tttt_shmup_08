using UnityEngine;

public abstract class Despawn : GilMonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    protected virtual void Despawning()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObject();
    }
    protected virtual void DespawnObject()
    {
        // if (this.transform.position.y < -10f)
        // {
        //     this.gameObject.SetActive(false);
        //     this.transform.position = new Vector3(0, 0, 0);
        // }

        Destroy(transform.parent.gameObject);
        // Hủy đối tượng cha
    }

    protected abstract bool CanDespawn();
    // Phương thức này sẽ được override trong các lớp con để xác định điều kiện hủy đối tượng
}
