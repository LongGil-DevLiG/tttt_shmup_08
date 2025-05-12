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
    //     if (this.junkCtrl == null) return;
    //     // Kiểm tra xem junkCtrl đã được gán hay chưa
    //     if (this.junkCtrl.JunkSpawner == null) return;
    //     // Kiểm tra xem JunkSpawner đã được gán hay chưa

    //     int randomIndex = Random.Range(0, this.junkCtrl.JunkSpawner.Prefabs.Count);
    //     // Tạo một chỉ số ngẫu nhiên trong khoảng từ 0 đến số lượng prefab rác

    //     this.junkCtrl.JunkSpawner.Spawn(randomIndex);
    //     // Gọi phương thức Spawn để sinh ra rác với chỉ số ngẫu nhiên
    // 
        Vector3 pos = transform.position;
        // Lấy vị trí của đối tượng hiện tại
        Quaternion ros = transform.rotation;
        // Lấy góc quay của đối tượng hiện tại
        Transform obj = this.junkCtrl.JunkSpawner.SpawnPrefab(JunkSpawner.JunkPrefabIndex, pos, ros);
        // Gọi phương thức SpawnPrefab để sinh ra rác với chỉ số ngẫu nhiên, vị trí và góc quay
        obj.gameObject.SetActive(true);
        // Kích hoạt đối tượng rác sau khi sinh ra
        Invoke("JunkSpawning", 1f);
        // Gọi lại phương thức JunkSpawning sau 1 giây để tiếp tục sinh ra rác
    }
}
