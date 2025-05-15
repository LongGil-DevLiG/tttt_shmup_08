using Unity.VisualScripting;
using UnityEngine;

public class BulletCtrl : GilMonoBehaviour
{
    [SerializeField] protected DamageSender damageSender;
    // Tham chiếu đến DamageSender để gửi sát thương
    public DamageSender DamageSender => this.damageSender;
    // Thuộc tính để truy cập DamageSender từ bên ngoài

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDamageSender();
        // Gọi phương thức để tải thành phần DamageSender
    }

    protected virtual void LoadDamageSender()
    {
        if (this.damageSender != null) return;
        // this.damageSender = GetComponent<DamageSender>();
        // if (this.damageSender == null)
        // {
        //     this.damageSender = GetComponentInChildren<DamageSender>();
        // }
        this.damageSender = GetComponentInChildren<DamageSender>();
        Debug.Log(transform.name + " :BulletCtrl load DamageSender", gameObject);
    }
}
