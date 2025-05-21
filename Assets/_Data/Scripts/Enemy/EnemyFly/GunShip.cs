using System.Collections;
using UnityEngine;

public class Gunship : BaseEnemy
{
    [Header("Gunship Specific")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] gunPoints;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float stopDuration = 1.5f;
    
    private bool isShooting = false;
    private float screenWidth;
    private float direction = 1f; // 1 = right, -1 = left
    
    protected override void Awake()
    {
        base.Awake();
        
        // Set stats
        moveSpeed = 3f;
        scoreValue = 100;
    }
    
    private void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        StartCoroutine(MovementPattern());
    }
    
    private IEnumerator MovementPattern()
    {
        while (!isDead)
        {
            // Move phase
            float moveTime = 0f;
            while (moveTime < moveDuration)
            {
                MovePattern();
                moveTime += Time.deltaTime;
                yield return null;
            }
            
            // Stop and shoot phase
            isShooting = true;
            yield return new WaitForSeconds(stopDuration);
            isShooting = false;
        }
    }
    
    public override void MovePattern()
    {
        if (isShooting)
        {
            Attack();
            return;
        }
        
        // Move horizontally
        transform.parent.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
        
        // Change direction if reaching screen edges
        if (transform.parent.position.x > screenWidth - 1f)
        {
            direction = -1f;
        }
        else if (transform.parent.position.x < -screenWidth + 1f)
        {
            direction = 1f;
        }
    }
    
    public override void Attack()
    {
        if (!isShooting) return;
        
        StartCoroutine(FireBullets());
    }
    
    private IEnumerator FireBullets()
    {
        isShooting = false; // Prevent multiple concurrent calls
        
        foreach (Transform gunPoint in gunPoints)
        {
            Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate / gunPoints.Length);
        }
        
        yield return new WaitForSeconds(fireRate);
        isShooting = true; // Re-enable shooting after cooldown
    }
}