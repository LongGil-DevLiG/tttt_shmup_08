using UnityEngine;

public class JunkCtrl : GilMonoBehaviour
{
    [SerializeField] protected JunkSpawner junkSpawner;
    // Tham chiếu đến JunkSpawner để quản lý việc sinh ra rác
    public JunkSpawner JunkSpawner => this.junkSpawner;
    // Thuộc tính để truy cập JunkSpawner từ bên ngoài
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkSpawner();
    }

    protected virtual void LoadJunkSpawner()
    {
        if (this.junkSpawner != null) return;
        this.junkSpawner = GetComponent<JunkSpawner>();
        Debug.Log(transform.name + " :JunkCtrl load JunkSpawner", gameObject);
    }

}
