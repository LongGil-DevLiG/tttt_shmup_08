using UnityEngine;

public class FastScout : BaseEnemy
{
    [Header("Scout Specific")]
    [SerializeField] private float accelerationRate = 1.2f;
    
    private Transform playerTransform;
    private Vector3 direction;
    
    protected override void Awake()
    {
        base.Awake();
        
        // Set stats
        moveSpeed = 5f;
        scoreValue = 50;
        
        // Tag for collision handling 
        // gameObject.tag = "Kamikaze";
        
        // Find player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Start()
    {
        // Set initial direction toward player position
        if (playerTransform != null)
        {
            direction = (playerTransform.position - transform.position).normalized;
        }
        else
        {
            direction = Vector3.down;
        }
        // direction = Vector3.down;
    }
    
    private void Update()
    {
        MovePattern();
    }
    
    public override void MovePattern()
    {
        // Constantly move down the screen at increasing speed
        moveSpeed *= 1 + (accelerationRate * Time.deltaTime * 0.01f);
        transform.parent.Translate(direction * moveSpeed * Time.deltaTime);
        
        // // Destroy if out of screen bounds
        // if (transform.position.y < -10f)
        // {
        //     Destroy(gameObject);
        //     waveManager.EnemyDestroyed();
        // }
    }
    
    public override void Attack()
    {
        // Fast Scout attacks by ramming into the player, handled in OnTriggerEnter2D
    }
}