using UnityEngine;

public class GilMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    protected virtual void Reset()
    {
        this.LoadComponents();
    }
    protected virtual void LoadComponents()
    {
        // Override this method in derived classes to load components
    }

}
