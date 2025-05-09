using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] protected Vector3 targetPosition = new Vector3(0, -5, 0);
    [SerializeField] protected float speed = 20f;

    void FixedUpdate()
    {
         this.GetTargetPosition();
         this.Moveing();
    }

    protected virtual void GetTargetPosition()
    {
         this.targetPosition = InputManager.Instance.TouchWorldPos; 
         this.targetPosition.z = 0f;
    }


    protected virtual void Moveing()
    {
         Vector3 newPos = Vector3.Lerp(transform.parent.position, this.targetPosition, this.speed * Time.deltaTime);
         transform.parent.position = newPos;
    }
}
