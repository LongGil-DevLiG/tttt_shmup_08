using UnityEngine;

public class OrbitalBulletFly : GilMonoBehaviour
{
    [SerializeField] private float orbitSpeed = 90f;     // Rotation speed in degrees per second
    [SerializeField] private float orbitRadius = 1.5f;     // Distance from player
    [SerializeField] protected Transform playerTransform;  // Vị trí Đối tượng mục tiêu để đạn bay xunh quanh

    private float currentAngle = 90f;     // Current angle of orbit
    
    protected override void LoadComponents()
    {
        this.LoadTargetForOrbitalBullet();
    }

    protected virtual void LoadTargetForOrbitalBullet()
    {
        if (this.playerTransform != null) return;

        // Tìm đối tượng MainShip trong scene
        GameObject ship = GameObject.FindWithTag("Ship");
        if (ship != null)
        {
            this.playerTransform = ship.transform;
        }
        else
        {
            Debug.LogError("No Ship found in the scene.");
        }
    }
    // private void Start()
    // {
    
    //     if (player != null)
    //     {
    //         playerTransform = player.transform;
            
    //         // Set random starting position around the player
    //         currentAngle = Random.Range(0f, 360f);
    //         UpdatePosition();
            
    //     }
    //     else
    //     {
    //         Debug.LogError("Player not found! Make sure your player has the 'Player' tag.");
    //         Destroy(transform.parent.gameObject);
    //     }
    // }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Update the angle based on orbit speed
            currentAngle += orbitSpeed * Time.deltaTime;
            
            // Keep angle between 0-360
            if (currentAngle >= 360f)
                currentAngle -= 360f;
                
            // Update bullet position
            UpdatePosition();
        }
    }
    
    private void UpdatePosition()
    {
        // Calculate the new position around the player
        float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitRadius;
        float y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitRadius;
        
        // Position bullet relative to player
        Vector3 offset = new Vector3(x, y, 0);
        transform.parent.position = playerTransform.position + offset;
        
        // Optional: make bullet face the direction of movement
        float rotationZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90f;
        transform.parent.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
