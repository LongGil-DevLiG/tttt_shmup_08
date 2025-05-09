using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // [SerializeField] private float lifetime = 1f;
    
    private void Start()
    {
        // Destroy(transform.parent.gameObject, lifetime);
    }

    private void Update()
    {
        transform.parent.Translate(Vector3.up * this.speed * Time.deltaTime);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Enemy"))
    //     {
    //         Destroy(other.gameObject);
    //         Destroy(transform.parent.gameObject);
    //     }
    // }
}
