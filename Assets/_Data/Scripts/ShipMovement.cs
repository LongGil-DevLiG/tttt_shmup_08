using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] protected Vector3 worldPosition = new Vector3(0, -5, 0);
    [SerializeField] protected float speed = 20f;

    void FixedUpdate()
    {

         this.worldPosition = InputManager.Instance.MouseWorldPos; 
         this.worldPosition.z = 0f;
         Vector3 newPos = Vector3.Lerp(transform.parent.position, this.worldPosition, this.speed * Time.deltaTime);
         transform.parent.position = newPos;
    }
}
