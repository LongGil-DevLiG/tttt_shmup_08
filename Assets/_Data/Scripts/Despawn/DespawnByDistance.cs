using UnityEngine;

public class DespawnByDistance : Despawn
{
    [Header("Despawn Config")]
    [SerializeField] protected float despawnDistance = 12f;  // Khoảng cách để hủy đối tượng
    [SerializeField] protected Transform target;  // Đối tượng mục tiêu để tính khoảng cách

    protected override void LoadComponents()
    {
        this.LoadShipDespawnByDistance();
    }

    protected virtual void LoadShipDespawnByDistance()
    {
        if (this.target != null) return;

        // Tìm đối tượng MainShip trong scene
        GameObject ship = GameObject.FindWithTag("Ship");
        if (ship != null)
        {
            this.target = ship.transform;
        }
        else
        {
            Debug.LogError("No Ship found in the scene.");
        }
    }

    protected override bool CanDespawn()
    {
        if (target == null) return false;

        // Tính khoảng cách giữa đối tượng và mục tiêu
        float distance = Vector3.Distance(transform.position, target.position);
        return distance > despawnDistance;
    }
}
