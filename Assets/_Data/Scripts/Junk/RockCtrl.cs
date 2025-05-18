using UnityEngine;

public class RockCtrl : GilMonoBehaviour
{
    [SerializeField] private JunkDespawn junkDespawn;
    // Tham chiếu đến JunkDespawn để quản lý việc hủy đối tượng đá
    public JunkDespawn JunkDespawn => this.junkDespawn;
    // Thuộc tính để truy cập JunkDespawn từ bên ngoài

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkDespawn();
        // Gọi phương thức để tải thành phần JunkDespawn
    }

    protected virtual void LoadJunkDespawn()
    {
        if (this.junkDespawn != null) return;
        this.junkDespawn = GetComponentInChildren<JunkDespawn>();
        Debug.Log(transform.name + " :RockCtrl load JunkDespawn", gameObject);
        // Tải thành phần JunkDespawn từ đối tượng hiện tại
    }
}
