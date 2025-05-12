using UnityEngine;

public class JunkFly : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    // public float rotationSpeed = 100f;

    private void Update()
    {
        // Move the object forward
        transform.parent.Translate(Vector3.down * speed * Time.deltaTime);

        // Rotate the object
        // transform.parent.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
