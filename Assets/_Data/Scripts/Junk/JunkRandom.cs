using UnityEngine;

public class JunkRandom : GilMonoBehaviour
{
    [SerializeField] protected JunkCtrl junkCtrl;
    // Tham chiếu đến JunkCtrl để quản lý rác

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkCtrl();
        // Tải các thành phần cần thiết
    }

    protected virtual void LoadJunkCtrl()
    {
        if (this.junkCtrl != null) return;
        this.junkCtrl = GetComponent<JunkCtrl>();
        Debug.Log(transform.name + " :JunkRandom load JunkCtrl", gameObject);
    }

    protected override void Start()
    {
        this.JunkSpawning();
    }

    protected virtual void JunkSpawning()
    {
        Transform randomSpawnPoint = this.junkCtrl.JunkSpawnPoints.GetRandom();
        // Lấy một điểm sinh ngẫu nhiên từ SpawnPoints
        Vector3 pos = randomSpawnPoint.position;
        // Lấy vị trí của đối tượng hiện tại
        Quaternion ros = transform.rotation;
        // Lấy góc quay của đối tượng hiện tại
        Transform obj = this.junkCtrl.JunkSpawner.SpawnPrefab(JunkSpawner.JunkPrefabIndex, pos, ros);
        // Gọi phương thức SpawnPrefab để sinh ra rác với chỉ số ngẫu nhiên, vị trí và góc quay
        obj.gameObject.SetActive(true);
        // Kích hoạt đối tượng rác sau khi sinh ra
        Invoke("JunkSpawning", 2f);
        // Gọi lại phương thức JunkSpawning sau N giây để tiếp tục sinh ra rác
    }
}
