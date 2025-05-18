using UnityEngine;

public class DamageReceiver : GilMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected float health = 100f;
    // Hp của đối tượng
    [SerializeField] protected float maxHealth = 100f;
    // Giá trị hp tối đa
    // [SerializeField] protected bool isDead = false;
    // Kiểm tra xem đối tượng đã chết hay chưa

    protected virtual void OnEnable()
    {
        this.ResetHealth();
        // Đặt lại hp khi đối tượng được kích hoạt
    }

    public virtual void ResetHealth()
    {
        this.health = this.maxHealth;
        // Đặt lại hp về giá trị tối đa
    }

    public virtual void AddHealth(float value)
    {
        this.health += value;
        // Tăng hp
        if (this.health > this.maxHealth)
        {
            this.health = this.maxHealth;
            // Đảm bảo Hp không vượt quá giá trị tối đa
        }
    }

    public virtual void DeductHealth(float value)
    {
        this.health -= value;
        // Giảm sức hp
        if (this.health < 0f)
        {
            this.health = 0f;
            // Đảm bảo hp không dưới 0
        }
        if (this.IsDead())
        {
            this.OnDead();
            // Gọi phương thức xử lý khi đối tượng chết
        }
    }

    protected virtual bool IsDead()
    {
        return this.health <= 0f;
        // Kiểm tra xem đối tượng đã chết hay chưa
    }

    protected virtual void OnDead()
    {
        // Xử lý khi đối tượng chết
        // this.DestroyObject();
        // Gọi phương thức hủy đối tượng
    }
    // private void DestroyObject()
    // {
    //      Destroy(transform.parent.gameObject);
    //     // Hủy đối tượng hiện tại
    //     // Debug.LogWarning(transform.name + " :DamageReceiver DestroyObject", gameObject);
    // }
}
