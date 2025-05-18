using Unity.VisualScripting;
using UnityEngine;

public class BulletCtrl : GilMonoBehaviour
{
    [Header("Damage Sender")]
    // Tham chiếu đến DamageSender để gửi sát thương
    [SerializeField] protected DamageSender damageSender;
    // Tham chiếu đến DamageSender để gửi sát thương
    public DamageSender DamageSender => this.damageSender;
    // Thuộc tính để truy cập DamageSender từ bên ngoài

    [Header("Bullet Despawn")]
    [SerializeField] protected BulletDespawn despawnBullet;
    // Tham chiếu đến BulletDespawn để hủy đối tượng đạn
    public BulletDespawn BulletDespawn => this.despawnBullet;

    // [Header("Orbital Bullet Despawn")]
    // [SerializeField] protected OrbitalBulletDespawn orbitalBulletDespawn;
    // // Tham chiếu đến OrbitalBulletDespawn để hủy đối tượng đạn
    // public OrbitalBulletDespawn OrbitalBulletDespawn => this.orbitalBulletDespawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDamageSender();
        // Gọi phương thức để tải thành phần DamageSender
        this.LoadBulletDespawn();
        // Gọi phương thức để tải thành phần BulletDespawn
        // this.LoadOrbitalBulletDespawn();
        // Gọi phương thức để tải thành phần OrbitalBulletDespawn
    }

    protected virtual void LoadDamageSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = GetComponentInChildren<DamageSender>();
        Debug.Log(transform.name + " :BulletCtrl load DamageSender", gameObject);
    }
    protected virtual void LoadBulletDespawn()
    {
        if (this.despawnBullet != null) return;
        this.despawnBullet = GetComponentInChildren<BulletDespawn>();
        Debug.Log(transform.name + " :BulletCtrl load BulletDespawn", gameObject);
    }
    // protected virtual void LoadOrbitalBulletDespawn()
    // {
    //     if (this.orbitalBulletDespawn != null) return;
    //     this.orbitalBulletDespawn = GetComponentInChildren<OrbitalBulletDespawn>();
    //     Debug.Log(transform.name + " :BulletCtrl load OrbitalBulletDespawn", gameObject);
    // }
}
