using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] protected bool isShooting = true;
    // Biến này có thể được điều chỉnh từ Inspector để bật/tắt bắn
    
    [Header("Shooting Config")]
    [SerializeField] protected float shootingDelay = 0.5f;  // Thời gian giữa các phát bắn
    [SerializeField] protected float minShootingDelay = 0.1f;  // Giới hạn tối thiểu của delay
    protected float shootingTimer = 0f;  // Đếm thời gian giữa các phát bắn

    void FixedUpdate()
    {
        this.Shooting();
    }

    protected virtual void Shooting()
    {
        if (!this.isShooting) return;

        // Kiểm tra và cập nhật timer
        if (shootingTimer > 0)
        {
            shootingTimer -= Time.fixedDeltaTime;
            return;
        }

        // Bắn đạn
        Transform newBullet = BulletSpawner.Instance.SpawnPrefab(BulletSpawner.BulletPrefabIndex, transform.position, Quaternion.identity);
        if (newBullet == null) return;
        
        newBullet.gameObject.SetActive(true);
        shootingTimer = shootingDelay;  // Reset timer
    }

    // Phương thức để tăng tốc độ bắn (giảm delay)
    public virtual void IncreaseFireRate(float decreaseAmount)
    {
        shootingDelay = Mathf.Max(minShootingDelay, shootingDelay - decreaseAmount);
    }

    // Phương thức để reset về tốc độ bắn mặc định
    public virtual void ResetFireRate(float defaultDelay = 0.5f)
    {
        shootingDelay = defaultDelay;
    }
}
