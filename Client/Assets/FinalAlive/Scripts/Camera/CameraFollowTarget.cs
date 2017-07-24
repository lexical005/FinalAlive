using UnityEngine;
using System.Collections;

/// <summary>
/// 摄像机跟随指定目标身上的特定节点，摄像机视野由跟随目标的视野决定
/// </summary>
public class CameraFollowTarget : MonoBehaviour
{

    /// <summary>
    /// 摄像机在哪个时机进行更新位置和旋转
    /// </summary>
    [System.Serializable]
    private enum UpdateMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    /// <summary>
    /// 摄像机在哪个时机进行更新位置和旋转
    /// </summary>
    [SerializeField]
    private UpdateMode updateMode = UpdateMode.LateUpdate;

    /// <summary>
    /// 摄像机跟随的目标，摄像机的视野，由跟随的目标的视野决定
    /// </summary>
    public CharacterObject m_FollowCharacter;

    /// <summary>
    /// 摄像机跟随的节点，摄像机的视野的中心线，总是会经过该节点，且摄像机会保持与该节点的距离
    /// </summary>
    public Transform m_FollowTarget;

    /// <summary>
    /// 是否锁定鼠标在本应用内
    /// </summary>
    public bool lockCursor = true;

    /// <summary>
    /// 是否平滑跟随
    /// </summary>
    public bool smoothFollow = false;

    /// <summary>
    /// 摄像机平滑跟随的速度
    /// </summary>
    [SerializeField]
    private float followSpeed = 10f;

    /// <summary>
    /// 摄像机距离跟随节点的距离
    /// 在编辑器下，可通过滚轮调节距离。在发布版，距离被锁定为该值。
    /// </summary>
    [SerializeField]
    private float distance = 10.0f;

#if UNITY_EDITOR
    /// <summary>
    /// 摄像机距离跟随节点的最短距离限定
    /// </summary>
    [SerializeField]
    private float minDistance = 4;

    /// <summary>
    /// 摄像机距离跟随节点的最短距离限定
    /// </summary>
    [SerializeField]
    private float maxDistance = 10;

    /// <summary>
    /// 摄像机距离跟随节点的距离进行滚轮缩放调整时的速度
    /// </summary>
    public float zoomSpeed = 10f;

    /// <summary>
    /// 滚轮缩放滚动时的缩放单位
    /// </summary>
    public float zoomSensitivity = 1f;

    public float distanceTarget { get; private set; } // Get/set distance
#endif

    public float rotationSensitivity = 3.5f; // The sensitivity of rotation

    /// <summary>
    /// 理论摄像机位置处进行此偏移
    /// </summary>
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0f, 0f);


    private Vector3 position;
    private Quaternion rotation = Quaternion.identity;
    private Vector3 smoothPosition;
    private Camera cam;

    // Initiate, set the params to the current transformation of the camera relative to the target
    protected virtual void Awake()
    {
#if UNITY_EDITOR
        distanceTarget = distance;
#endif

        smoothPosition = m_FollowTarget.position;

        cam = GetComponent<Camera>();
    }

    protected virtual void Update()
    {
        if (updateMode == UpdateMode.Update)
        {
            UpdateTransform();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (updateMode == UpdateMode.FixedUpdate)
        {
            UpdateTransform();
        }
    }

    protected virtual void LateUpdate()
    {
        UpdateInput();

        if (updateMode == UpdateMode.LateUpdate)
        {
            UpdateTransform();
        }
    }

    /// <summary>
    /// 读取跟随目标以及节点的信息，设定摄像机目标
    /// </summary>
    public void UpdateInput()
    {
        if (m_FollowTarget == null || m_FollowCharacter == null)
        {
            return;
        }

#if UNITY_EDITOR
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = lockCursor ? false : true;

        // 重新计算距离跟随节点的距离
        distanceTarget = Mathf.Clamp(distanceTarget + ZoomValue, minDistance, maxDistance);
#endif
    }

    // Update the camera transform
    public void UpdateTransform()
    {
        UpdateTransform(Time.deltaTime);
    }

    public void UpdateTransform(float deltaTime)
    {
        if (m_FollowTarget == null || m_FollowCharacter == null)
        {
            return;
        }

        // 距离
#if UNITY_EDITOR
        distance += (distanceTarget - distance) * zoomSpeed * deltaTime;
#endif

        // Rotation
        rotation = m_FollowCharacter.transform.rotation;

        // Smooth follow
        if (!smoothFollow)
        {
            smoothPosition = m_FollowTarget.position;
        }
        else
        {
            smoothPosition = Vector3.Lerp(smoothPosition, m_FollowTarget.position, deltaTime * followSpeed);
        }

        // Position
        position = smoothPosition + rotation * (offset - distance * Vector3.forward);

        // Translating the camera
        transform.position = position;
        transform.rotation = rotation;
    }

#if UNITY_EDITOR
    /// <summary>
    /// 滚轮修正
    /// </summary>
    private float ZoomValue
    {
        get
        {
            float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
            if (scrollAxis > 0)
            {
                return -zoomSensitivity;
            }

            if (scrollAxis < 0)
            {
                return zoomSensitivity;
            }

            return 0;
        }
    }
#endif
}
