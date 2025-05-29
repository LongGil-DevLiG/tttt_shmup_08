using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Input Positions")]
    [SerializeField] protected Vector3 touchWorldPos;
    public Vector3 TouchWorldPos { get => touchWorldPos; }

    [SerializeField] protected Vector3 mouseWorldPos;
    public Vector3 MouseWorldPos { get => mouseWorldPos; }

    [Header("Slow Motion Settings")]
    [SerializeField] private float normalTimeScale = 1f;
    [SerializeField] private float slowTimeScale = 0.1f;
    [SerializeField] private float slowDownSpeed = 2f; // Tốc độ chậm dần
    [SerializeField] private float speedUpSpeed = 3f; // Tốc độ tăng tốc khi có input
    [SerializeField] private float inputTimeoutDuration = 2f; // Thời gian chờ trước khi bắt đầu chậm dần
    
    [Header("UI Slow Motion Settings")]  
    [SerializeField] private float uiSlowFactor = 0.3f; // UI chậm nhưng không chậm bằng gameplay
    
    // Input tracking variables
    private float lastInputTime;
    private bool hasRecentInput;
    private float currentTimeScale;
    private float targetTimeScale;
    
    // UI Animation variables
    private CanvasGroup[] uiCanvasGroups;
    private Animator[] uiAnimators;
    
    // Events
    public System.Action<float> OnTimeScaleChanged;
    public System.Action<bool> OnSlowMotionStateChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSlowMotion();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeSlowMotion()
    {
        currentTimeScale = normalTimeScale;
        targetTimeScale = normalTimeScale;
        lastInputTime = Time.unscaledTime;
        hasRecentInput = true;
        
        // Tìm tất cả UI elements để áp dụng slow motion
        uiCanvasGroups = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None);
        uiAnimators = FindObjectsByType<Animator>(FindObjectsSortMode.None);
    }
    
    void FixedUpdate()
    {
        this.GetTouchPosition();
        this.UpdateSlowMotion();
    }
    
    void Update()
    {
        // Update slow motion trong Update để có framerate smooth hơn
        this.ApplyTimeScale();
    }

    protected virtual void GetTouchPosition()
    {
        Vector3 touchPosition = Vector3.zero;
        bool validTouch = false;

        // Ưu tiên xử lý cảm ứng đa điểm trên thiết bị di động
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                touchPosition = touch.position;
                validTouch = true;
                RegisterInput(); // Đăng ký input để reset slow motion
            }
        }
        else if (Input.GetMouseButton(0))
        {
            touchPosition = Input.mousePosition;
            validTouch = true;
            RegisterInput(); // Đăng ký input để reset slow motion
        }
        else if (Input.GetMouseButtonUp(0))
        {
            validTouch = false;
        }
        
        // Kiểm tra input từ keyboard để cũng reset slow motion
        if (Input.anyKeyDown)
        {
            RegisterInput();
        }
        
        // Chỉ cập nhật vị trí khi có touch hợp lệ
        if (validTouch && Camera.main != null)
        {
            if (touchPosition.x >= 0 && touchPosition.y >= 0 && 
                touchPosition.x <= Screen.width && touchPosition.y <= Screen.height)
            {
                touchPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                this.touchWorldPos = Camera.main.ScreenToWorldPoint(touchPosition);
                
                #if UNITY_WEBGL
                HandleWebGLSpecificBehavior();
                #endif
            }
        }
    }
    
    /// <summary>
    /// Đăng ký khi có input từ người dùng
    /// </summary>
    private void RegisterInput()
    {
        lastInputTime = Time.unscaledTime;
        if (!hasRecentInput)
        {
            hasRecentInput = true;
            targetTimeScale = normalTimeScale;
            OnSlowMotionStateChanged?.Invoke(false);
        }
    }
    
    /// <summary>
    /// Cập nhật logic slow motion
    /// </summary>
    private void UpdateSlowMotion()
    {
        float timeSinceLastInput = Time.unscaledTime - lastInputTime;
        
        // Nếu không có input trong thời gian timeout, bắt đầu chậm dần
        if (timeSinceLastInput > inputTimeoutDuration && hasRecentInput)
        {
            hasRecentInput = false;
            targetTimeScale = slowTimeScale;
            OnSlowMotionStateChanged?.Invoke(true);
        }
    }
    
    /// <summary>
    /// Áp dụng time scale một cách mượt mà
    /// </summary>
    private void ApplyTimeScale()
    {
        // Smooth transition giữa các time scale
        if (hasRecentInput)
        {
            currentTimeScale = Mathf.Lerp(currentTimeScale, targetTimeScale, speedUpSpeed * Time.unscaledDeltaTime);
        }
        else
        {
            currentTimeScale = Mathf.Lerp(currentTimeScale, targetTimeScale, slowDownSpeed * Time.unscaledDeltaTime);
        }
        
        // Áp dụng time scale
        Time.timeScale = currentTimeScale;
        
        // Cập nhật UI với time scale riêng biệt
        UpdateUITimeScale();
        
        // Trigger event khi time scale thay đổi
        OnTimeScaleChanged?.Invoke(currentTimeScale);
    }
    
    /// <summary>
    /// Cập nhật time scale cho UI elements
    /// </summary>
    private void UpdateUITimeScale()
    {
        float uiTimeScale = Mathf.Lerp(1f, uiSlowFactor, 1f - (currentTimeScale / normalTimeScale));
        
        // Áp dụng cho UI Animators
        if (uiAnimators != null)
        {
            foreach (var animator in uiAnimators)
            {
                if (animator != null && animator.gameObject.activeInHierarchy)
                {
                    animator.speed = uiTimeScale;
                }
            }
        }
    }
    
    /// <summary>
    /// Force reset về time scale bình thường
    /// </summary>
    public void ForceNormalSpeed()
    {
        hasRecentInput = true;
        lastInputTime = Time.unscaledTime;
        targetTimeScale = normalTimeScale;
        currentTimeScale = normalTimeScale;
        Time.timeScale = normalTimeScale;
        
        // Reset UI animators
        if (uiAnimators != null)
        {
            foreach (var animator in uiAnimators)
            {
                if (animator != null)
                {
                    animator.speed = 1f;
                }
            }
        }
        
        OnSlowMotionStateChanged?.Invoke(false);
    }
    
    /// <summary>
    /// Force chuyển sang slow motion ngay lập tức
    /// </summary>
    public void ForceSlowMotion()
    {
        hasRecentInput = false;
        targetTimeScale = slowTimeScale;
        OnSlowMotionStateChanged?.Invoke(true);
    }
    
    /// <summary>
    /// Lấy trạng thái hiện tại của slow motion
    /// </summary>
    public bool IsInSlowMotion()
    {
        return !hasRecentInput && currentTimeScale < normalTimeScale * 0.9f;
    }
    
    /// <summary>
    /// Lấy tỷ lệ slow motion hiện tại (0-1)
    /// </summary>
    public float GetSlowMotionRatio()
    {
        return 1f - (currentTimeScale / normalTimeScale);
    }

#if UNITY_WEBGL
    /// <summary>
    /// Xử lý đặc biệt cho nền tảng WebGL (itch.io)
    /// </summary>
    private void HandleWebGLSpecificBehavior()
    {
        // Điều chỉnh tỷ lệ nếu cần thiết cho WebGL trên itch.io
        // Có thể cần điều chỉnh slow motion cho WebGL để tránh lag
        if (IsInSlowMotion())
        {
            // WebGL có thể cần time scale cao hơn để tránh stuttering
            float webglAdjustment = 1.2f;
            Time.timeScale = Mathf.Max(currentTimeScale * webglAdjustment, 0.05f);
        }
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

    protected virtual void GetMousePosition()
    {
        this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void OnDestroy()
    {
        // Reset time scale khi destroy để tránh ảnh hưởng đến scene khác
        Time.timeScale = 1f;
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            // Khi game resume, reset input time để tránh slow motion ngay lập tức
            RegisterInput();
        }
    }
}