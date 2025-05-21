using UnityEngine;

public class EnemyCtrl : GilMonoBehaviour
{
    [Header("Enemy Controller")]
    [SerializeField] private EnemyDespawn enemyDespawn;
    // Tham chiếu đến EnemyDespawn để quản lý việc hủy đối tượng đá
    public EnemyDespawn EnemyDespawn => this.enemyDespawn;
    // Thuộc tính để truy cập EnemyDespawn từ bên ngoài

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyDespawn();
    }

    protected virtual void LoadEnemyDespawn()
    {
        if (this.enemyDespawn != null) return;
        this.enemyDespawn = GetComponentInChildren<EnemyDespawn>();
        Debug.Log($"Load EnemyDespawn: {this.enemyDespawn}", this);
    }

}
