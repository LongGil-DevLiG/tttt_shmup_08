using UnityEngine;

public class DamageSender : MonoBehaviour
{
    [SerializeField] protected float damage = 50f;
    // Giá trị sát thương

    public virtual void SendDamage(Transform objectToDamage)
    {
        if (objectToDamage == null) return;
        DamageReceiver damageReceiver = objectToDamage.GetComponent<DamageReceiver>();
        // Lấy thành phần DamageReceiver từ đối tượng nhận sát thương
        if (damageReceiver == null) return;
        this.SendDamage(damageReceiver);
        // Gửi sát thương đến đối tượng nhận sát thương
    }

    public virtual void SendDamage(DamageReceiver damageReceiver)
    {
        if (damageReceiver == null) return;
        damageReceiver.DeductHealth(this.damage);
        // Gửi sát thương đến đối tượng nhận sát thương
        this.DestroyObject();
        // Gọi phương thức hủy đối tượng
    }

    protected virtual void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
        // Hủy đối tượng cha
    }
}
