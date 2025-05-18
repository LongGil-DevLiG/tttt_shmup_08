using System.Collections;
using UnityEngine;

public class BulletDamageSender : DamageSender
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    // Tham chiếu đến BulletCtrl để quản lý đạn

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
        Debug.Log(transform.name + " :BulletDamageSender load BulletCtrl", gameObject);
    }

    public override void SendDamage(DamageReceiver damageReceiver)
    {
        base.SendDamage(damageReceiver);
        // Gửi sát thương đến đối tượng nhận sát thương
        this.DespawnBullet();
        // Gọi phương thức hủy đạn
    }

    protected virtual void DespawnBullet()
    {
        
        if (this.bulletCtrl == null)
        {
            Debug.LogError("bulletCtrl is null!", gameObject);
            return;
        }

        if (this.bulletCtrl.BulletDespawn == null)
        {
            Debug.LogError("BulletDespawn is null!", gameObject);
            return;
        }

        this.bulletCtrl.BulletDespawn.DespawnObject();
        // Debug.LogWarning("Despawn bullet by distance");

            // this.bulletCtrl.OrbitalBulletDespawn.DespawnObject();
            // Gọi phương thức hủy đối tượng đạn despawn boi time
        
    }
}