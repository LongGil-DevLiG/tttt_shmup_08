using UnityEngine;

public class JunkCtrl : GilMonoBehaviour
{
    [SerializeField] protected JunkSpawner junkSpawner;
    // Tham chiếu đến JunkSpawner để quản lý việc sinh ra rác
    public JunkSpawner JunkSpawner => this.junkSpawner;
    // Thuộc tính để truy cập JunkSpawner từ bên ngoài

    [SerializeField] protected JunkSpawnPoints junkSpawnPoints;
    // Tham chiếu đến SpawnPoints để quản lý các điểm sinh rác
    public JunkSpawnPoints JunkSpawnPoints => this.junkSpawnPoints;
    // Thuộc tính để truy cập SpawnPoints từ bên ngoài
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkSpawner();
        this.LoadSpawnPoints();
        // Gọi các phương thức để tải các thành phần JunkSpawner và SpawnPoints
    }

    protected virtual void LoadJunkSpawner()
    {
        if (this.junkSpawner != null) return;
        this.junkSpawner = GetComponent<JunkSpawner>();
        Debug.Log(transform.name + " :JunkCtrl load JunkSpawner", gameObject);
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.junkSpawnPoints != null) return;
        this.junkSpawnPoints = Transform.FindObjectsByType<JunkSpawnPoints>(FindObjectsSortMode.None)[0];
        Debug.Log(transform.name + " :JunkCtrl load SpawnPoints", gameObject);
    }

}
