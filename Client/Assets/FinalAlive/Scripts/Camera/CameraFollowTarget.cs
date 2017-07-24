using UnityEngine;
using System.Collections;

/// <summary>
/// ���������ָ��Ŀ�����ϵ��ض��ڵ㣬�������Ұ�ɸ���Ŀ�����Ұ����
/// </summary>
public class CameraFollowTarget : MonoBehaviour
{

    /// <summary>
    /// ��������ĸ�ʱ�����и���λ�ú���ת
    /// </summary>
    [System.Serializable]
    private enum UpdateMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    /// <summary>
    /// ��������ĸ�ʱ�����и���λ�ú���ת
    /// </summary>
    [SerializeField]
    private UpdateMode updateMode = UpdateMode.LateUpdate;

    /// <summary>
    /// ����������Ŀ�꣬���������Ұ���ɸ����Ŀ�����Ұ����
    /// </summary>
    public CharacterObject m_FollowCharacter;

    /// <summary>
    /// ���������Ľڵ㣬���������Ұ�������ߣ����ǻᾭ���ýڵ㣬��������ᱣ����ýڵ�ľ���
    /// </summary>
    public Transform m_FollowTarget;

    /// <summary>
    /// �Ƿ���������ڱ�Ӧ����
    /// </summary>
    public bool lockCursor = true;

    /// <summary>
    /// �Ƿ�ƽ������
    /// </summary>
    public bool smoothFollow = false;

    /// <summary>
    /// �����ƽ��������ٶ�
    /// </summary>
    [SerializeField]
    private float followSpeed = 10f;

    /// <summary>
    /// ������������ڵ�ľ���
    /// �ڱ༭���£���ͨ�����ֵ��ھ��롣�ڷ����棬���뱻����Ϊ��ֵ��
    /// </summary>
    [SerializeField]
    private float distance = 10.0f;

#if UNITY_EDITOR
    /// <summary>
    /// ������������ڵ����̾����޶�
    /// </summary>
    [SerializeField]
    private float minDistance = 4;

    /// <summary>
    /// ������������ڵ����̾����޶�
    /// </summary>
    [SerializeField]
    private float maxDistance = 10;

    /// <summary>
    /// ������������ڵ�ľ�����й������ŵ���ʱ���ٶ�
    /// </summary>
    public float zoomSpeed = 10f;

    /// <summary>
    /// �������Ź���ʱ�����ŵ�λ
    /// </summary>
    public float zoomSensitivity = 1f;

    public float distanceTarget { get; private set; } // Get/set distance
#endif

    public float rotationSensitivity = 3.5f; // The sensitivity of rotation

    /// <summary>
    /// ���������λ�ô����д�ƫ��
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
    /// ��ȡ����Ŀ���Լ��ڵ����Ϣ���趨�����Ŀ��
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

        // ���¼���������ڵ�ľ���
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

        // ����
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
    /// ��������
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
