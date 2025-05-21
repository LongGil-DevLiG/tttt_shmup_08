using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [Header("Enemy Damage Receiver")]
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        Debug.Log($"Load EnemyCtrl: {this.enemyCtrl}", this);
    }

    protected override void OnDead()
    {
        base.OnDead();
        this.enemyCtrl.EnemyDespawn.DespawnObject();
        // Debug.LogWarning("Despawn enemy by distance", this);
    }
}
