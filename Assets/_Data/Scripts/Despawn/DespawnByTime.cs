using UnityEngine;

public class DespawnByTime : Despawn
{
    [Header("Despawn Config")]
    [SerializeField] protected float despawnTime = 5f;  // Thời gian để hủy đối tượng

    private float spawnTime;
    // Thời gian mà đối tượng được tạo ra
    protected override void Awake()
    {
        this.spawnTime = Time.time;
        // Lưu thời gian hiện tại khi đối tượng được tạo ra
    }

    protected override bool CanDespawn()
    {
        return Time.time - this.spawnTime > this.despawnTime;
    }
}
