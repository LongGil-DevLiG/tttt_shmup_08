using UnityEngine;

public class DamageReceiver : GilMonoBehaviour
{
    [SerializeField] protected float health = 100f;
    // Hp của đối tượng

    [SerializeField] protected float maxHealth = 100f;
    // Giá trị hp tối đa

    protected override void Start()
    {
        base.Start();
        this.ResetHealth();
        // Đặt lại hp ban đầu
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
    }

    protected virtual bool IsDead()
    {
        return this.health <= 0f;
        // Kiểm tra xem đối tượng đã chết hay chưa
    }
}
