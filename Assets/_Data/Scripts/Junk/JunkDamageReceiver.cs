using UnityEngine;

public class JunkDamageReceiver : DamageReceiver
{
    [Header("Junk Damage Receiver")]
    [SerializeField] protected RockCtrl rockCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRockCtrl();
    }

    protected virtual void LoadRockCtrl()
    {
        if (this.rockCtrl != null) return;
        this.rockCtrl = transform.parent.GetComponent<RockCtrl>();
        Debug.Log($"Load RockCtrl: {this.rockCtrl}", this);
    }

    protected override void OnDead()
    {
        base.OnDead();
        this.rockCtrl.JunkDespawn.DespawnObject();
        Debug.LogWarning("Despawn junk by distance", this);
    }

}
