using UnityEngine;

public class DespawnByTime : Despawn
{
    [Header("Despawn Config")]
    [SerializeField] protected float despawnTime = 5f;  // Thời gian để hủy đối tượng

    private float spawnTime;

    // protected override void Awake()
    // {
    //     this.spawnTime = Time.time;
    //     // Lưu thời gian hiện tại khi đối tượng được tạo ra
    // }
    
    // Thời gian mà đối tượng được tạo ra
    private void OnEnable()
    {
        this.spawnTime = Time.time;
        // Lưu thời gian hiện tại mỗi khi đối tượng được kích hoạt
    }

    protected override bool CanDespawn()
    {
        return Time.time - this.spawnTime > this.despawnTime;
    }
}
