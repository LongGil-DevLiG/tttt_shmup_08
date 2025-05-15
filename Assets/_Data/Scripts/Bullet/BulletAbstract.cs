using UnityEngine;

public abstract class BulletAbstract : GilMonoBehaviour
{
    [Header("Bullet Abstract")]
    [SerializeField] protected BulletCtrl bulletCtrl;
    // Tham chiếu đến BulletCtrl để quản lý hành vi của viên đạn

    public BulletCtrl BulletCtrl => this.bulletCtrl;
    // Thuộc tính để truy cập BulletCtrl từ bên ngoài

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
        // Gọi phương thức để tải thành phần BulletCtrl
    }

    protected virtual void LoadBulletCtrl()
    {
        if (this.bulletCtrl != null) return;
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        Debug.Log(transform.name + " :BulletAbstract load BulletCtrl", gameObject);
        // Tải thành phần BulletCtrl từ đối tượng hiện tại
    }
}
