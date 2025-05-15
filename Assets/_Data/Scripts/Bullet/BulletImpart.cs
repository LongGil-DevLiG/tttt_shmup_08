using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BulletImpart : BulletAbstract
{
    [Header("Bullet Impart")]
    [SerializeField] protected CapsuleCollider2D capsulecollider2D;
    // Tham chiếu đến CapsuleCollider2D để quản lý va chạm của viên đạn
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    // Tham chiếu đến Rigidbody2D để quản lý vật lý của viên đạn

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // // Kiểm tra va chạm với các đối tượng khác
        // if (other.CompareTag("Player"))
        // {
        //     // Nếu va chạm với đối tượng có tag "Player"
        //     DamageReceiver damageReceiver = other.GetComponent<DamageReceiver>();
        //     if (damageReceiver != null)
        //     {
        //         // Nếu đối tượng có thành phần DamageReceiver
        //         damageReceiver.DeductHealth(10f);
        //         // Giảm sức khỏe của đối tượng
        //     }
        // }
    
        this.bulletCtrl.DamageSender.SendDamage(other.transform);
        // Gửi sát thương đến đối tượng va chạm
        Debug.Log(transform.name + " :BulletImpart OnTriggerEnter2D", gameObject);
        // Ghi lại thông tin va chạm
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCapsuleCollider2D();
        this.LoadRigidbody2D();
        // Gọi các phương thức để tải các thành phần CapsuleCollider2D và Rigidbody2D
    }
    protected virtual void LoadCapsuleCollider2D()
    {
        if (this.capsulecollider2D != null) return;
        this.capsulecollider2D = GetComponent<CapsuleCollider2D>();
        Debug.Log(transform.name + " :BulletImpart load CapsuleCollider2D", gameObject);
        // Tải thành phần CapsuleCollider2D từ đối tượng hiện tại
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this._rigidbody2D != null) return;
        this._rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + " :BulletImpart load Rigidbody2D", gameObject);
        // Tải thành phần Rigidbody2D từ đối tượng hiện tại
    }


}
