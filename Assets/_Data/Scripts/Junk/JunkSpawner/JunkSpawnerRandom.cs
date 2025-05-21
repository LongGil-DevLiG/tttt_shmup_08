using UnityEngine;

public class JunkSpawnerRandom : GilMonoBehaviour
{
    [SerializeField] protected JunkSpawnerCtrl junkCtrl;
    // Tham chiếu đến JunkCtrl để quản lý rác
    [SerializeField] private float timeToSpawn = 2.0f;
    // Thời gian để sinh ra rác
    [SerializeField] private float timeRun = 0f;
    // Thời gian chạy của rác

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadJunkCtrl();
        // Tải các thành phần cần thiết
    }

    protected virtual void LoadJunkCtrl()
    {
        if (this.junkCtrl != null) return;
        this.junkCtrl = GetComponent<JunkSpawnerCtrl>();
        Debug.Log(transform.name + " :JunkRandom load JunkCtrl", gameObject);
    }

    protected override void Start()
    {
        // this.JunkSpawning();
    }

    protected virtual void FixedUpdate()
    {
        this.JunkSpawning();
        // Gọi phương thức JunkSpawning trong mỗi khung hình
    }

    protected virtual void JunkSpawning()
    {
        this.timeRun += Time.fixedDeltaTime;
        // Cập nhật thời gian chạy
        if (this.timeRun < this.timeToSpawn) return;
        // Nếu thời gian chạy nhỏ hơn thời gian để sinh ra rác thì không làm gì cả
        this.timeRun = 0f;
        // Đặt lại thời gian chạy về 0 nếu tiếp tục sinh ra rác


        Transform randomSpawnPoint = this.junkCtrl.JunkSpawnPoints.GetRandom();
        // Lấy một điểm sinh ngẫu nhiên từ SpawnPoints
        Vector3 pos = randomSpawnPoint.position;
        // Lấy vị trí của đối tượng hiện tại
        Quaternion ros = transform.rotation;
        // Lấy góc quay của đối tượng hiện tại
        Transform obj = this.junkCtrl.JunkSpawner.SpawnPrefab(JunkSpawner.JunkPrefabIndex, pos, ros);
        // int randomIndex = JunkSpawner.Instance.GetRandomJunkPrefabIndex();
        // Transform junk = JunkSpawner.Instance.SpawnPrefab(randomIndex, pos, ros);
        // Gọi phương thức SpawnPrefab để sinh ra rác với chỉ số ngẫu nhiên, vị trí và góc quay
        obj.gameObject.SetActive(true);
        // Kích hoạt đối tượng rác sau khi sinh ra
        // Invoke("JunkSpawning", 2f);
        // Gọi lại phương thức JunkSpawning sau N giây để tiếp tục sinh ra rác
    }
}
