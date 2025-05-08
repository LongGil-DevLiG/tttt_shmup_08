using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get ; private set; }

    [SerializeField] protected Vector3 mouseWorldPos;
    public Vector3 MouseWorldPos { get => mouseWorldPos; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        // #if UNITY_ANDROID || UNITY_IOS
        //     this.GetTouchPosition();
        // #elif UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        //     this.GetMousePos();
        // #elif UNITY_WEBGL
        //     if (IsMobileDevice())
        //     {
        //         this.GetTouchPosition();
        //     }
        //     else
        //     {
        //         this.GetMousePos();
        //     }    
        // #endif
        this.GetTouchPosition();
    }

protected virtual void GetTouchPosition()
{
    Vector3 touchPosition = Vector3.zero;
    bool validTouch = false;

    // Ưu tiên xử lý cảm ứng đa điểm trên thiết bị di động
    if (Input.touchCount > 0)
    {
        // Lấy touch đầu tiên để di chuyển
        Touch touch = Input.GetTouch(0);
        
        // Chỉ xử lý khi người dùng chạm vào màn hình hoặc di chuyển ngón tay
        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            touchPosition = touch.position;
            validTouch = true;
        }
    }
    else if (Input.GetMouseButton(0)) // Nếu không có touch, kiểm tra chuột trái
    {
        touchPosition = Input.mousePosition;
        validTouch = true;
    }
    else if (Input.GetMouseButtonUp(0)) // Nếu nhả chuột trái, không cần xử lý
    {
        validTouch = false;
    }
    // Chỉ cập nhật vị trí khi có touch hợp lệ
    if (validTouch && Camera.main != null)
    {
        // Kiểm tra vị trí có nằm trong màn hình không
        if (touchPosition.x >= 0 && touchPosition.y >= 0 && 
            touchPosition.x <= Screen.width && touchPosition.y <= Screen.height)
        {
            // Định nghĩa z để đảm bảo chuyển đổi tọa độ chính xác
            touchPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            
            // Chuyển đổi tọa độ màn hình sang tọa độ thế giới
            this.mouseWorldPos = Camera.main.ScreenToWorldPoint(touchPosition);
            
            // Xử lý thêm cho WebGL nếu cần thiết
            #if UNITY_WEBGL
            HandleWebGLSpecificBehavior();
            #endif
        }
    }
}

#if UNITY_WEBGL
/// <summary>
/// Xử lý đặc biệt cho nền tảng WebGL (itch.io)
/// </summary>
private void HandleWebGLSpecificBehavior()
{
    // Điều chỉnh tỷ lệ nếu cần thiết cho WebGL trên itch.io
    // Ví dụ: Xử lý DPI scaling, xử lý viewport cho nhúng iframe, v.v.
    
    // Có thể thêm xử lý cho vấn đề phổ biến trên WebGL như:
    // - Điều chỉnh độ nhạy chuột
    // - Xử lý tương thích với các thiết bị cảm ứng
    // - Xử lý hiệu ứng trên thiết bị khác nhau
}
private bool IsMobileDevice()
{
    string userAgent = SystemInfo.deviceModel.ToLower();
    return userAgent.Contains("android") || 
           userAgent.Contains("iphone") || 
           userAgent.Contains("ipad") || 
           userAgent.Contains("mobile");
}
#endif

protected virtual void GetMousePos()
{
    this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
}


