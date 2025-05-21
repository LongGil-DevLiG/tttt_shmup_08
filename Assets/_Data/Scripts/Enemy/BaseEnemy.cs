using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int scoreValue;
    
    [Header("Effects")]
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected AudioClip deathSound;
    
    protected bool isDead = false;
    [SerializeField] protected WaveManager waveManager;
    
    protected virtual void Awake()
    {
        // currentHealth = maxHealth;
        // waveManager = FindObjectOfType<WaveManager>();
    }
    
    // public virtual void TakeDamage(int damageAmount)
    // {
    //     if (isDead) return;
        
    //     currentHealth -= damageAmount;
        
    //     if (currentHealth <= 0)
    //     {
    //         Die();
    //     }
    // }
    
    // protected virtual void Die()
    // {
    //     isDead = true;
        
    //     // Add score
    //     GameManager.Instance.AddScore(scoreValue);
        
    //     // Play explosion effect
    //     if (explosionPrefab != null)
    //     {
    //         Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    //     }
        
    //     // Play sound
    //     if (deathSound != null)
    //     {
    //         AudioSource.PlayClipAtPoint(deathSound, transform.position);
    //     }
        
    //     // Notify wave manager
    //     waveManager.EnemyDestroyed();
        
    //     // Destroy the object
    //     Destroy(gameObject);
    // }
    
    // protected virtual void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
    //         if (playerHealth != null)
    //         {
    //             playerHealth.TakeDamage(damage);
    //         }
            
    //         // Some enemies might die on collision, others might not
    //         if (CompareTag("Kamikaze"))
    //         {
    //             Die();
    //         }
    //     }
    // }
    
    // Method to be overridden by derived classes
    public abstract void MovePattern();
    public abstract void Attack();
}
