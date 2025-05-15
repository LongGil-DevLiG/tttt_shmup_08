using UnityEngine;

public class JunkFly : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 100f;

    private float randomZRotation;

    private void Start()
    {
        // Generate a random rotation angle for the Z axis
        randomZRotation = Random.Range(-1f, 1f);
    }

    private void Update()
    {
        // Di chuyển cha theo hướng cố định (Vector3.down) mà không bị ảnh hưởng bởi xoay
        transform.parent.position += Vector3.down * speed * Time.deltaTime;

        // Xoay cha quanh trục Z
        transform.parent.Rotate(0, 0, randomZRotation * rotationSpeed * Time.deltaTime);
    }
}
