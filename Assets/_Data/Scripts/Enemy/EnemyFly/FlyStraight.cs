using UnityEngine;

public class FlyStraight : BaseEnemy
{
    [Header("Fly Straight")]
    private Vector3 direction = Vector3.down;

    [SerializeField] private float moveSpeedDown = 1.2f;
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void Update()
    {
        MovePattern();
    }
    
    public override void MovePattern()
    {
        // Move straight down the screen
        transform.parent.Translate(direction * moveSpeedDown * Time.deltaTime);

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
