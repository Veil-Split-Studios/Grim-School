using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchController : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform modelTransform;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction crouchAction;

    [Header("Boy ve Kamera Y�ksekli�i")]
    [SerializeField] private float standHeight = 1.8f;
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private float standCameraY = 1.6f;
    [SerializeField] private float crouchCameraY = 1.0f;

    [Header("Yumu�ak Ge�i�")]
    [SerializeField] private float lerptime = 0.1f;

    private bool isCrouching = false;
    private Vector3 modelStartLocalPos;
    private float originalCenterY;

    private void Start()
    {
        if (modelTransform != null)
            modelStartLocalPos = modelTransform.localPosition;
        originalCenterY = gameObject.transform.position.y;
   
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        //"Crouch" InputAction'� bul
        crouchAction = playerInput.actions.FindAction("Crouch", true);

        //Tu�a bas�nca crouch a�
        crouchAction.started += ctx => isCrouching = true;
        //Tu� b�rak�l�nca crouch kapat
        crouchAction.canceled += ctx => isCrouching = false;
    }
    void Update()
    {
        // Hedef de�erler
        float targetHeight = isCrouching ? crouchHeight : standHeight;
        float targetCameraY = isCrouching ? crouchCameraY : standCameraY;
        float targetModelY = isCrouching
            ? modelStartLocalPos.y - (standHeight - crouchHeight) / 2f
            : modelStartLocalPos.y;

        // Sadece height'� de�i�tir, center sabit kals�n!
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime / lerptime);

        if (cameraRoot != null)
        {
            Vector3 camPos = cameraRoot.localPosition;
            camPos.y = Mathf.Lerp(camPos.y, targetCameraY, Time.deltaTime / lerptime);
            cameraRoot.localPosition = camPos;
        }

        if (modelTransform != null)
        {
            Vector3 modelPos = modelTransform.localPosition;
            modelPos.y = Mathf.Lerp(modelPos.y, targetModelY, Time.deltaTime / lerptime);
            modelTransform.localPosition = modelPos;
        }

        TryStandUp();

    }
    void TryStandUp()
    {
        // Sadece aya�a kalkarken kontrol et
        if (!isCrouching)
        {
            float headroom = standHeight - crouchHeight + 0.1f;
            Vector3 origin = transform.position + Vector3.up * (crouchHeight / 2f);
            if (Physics.SphereCast(origin, characterController.radius * 0.95f, Vector3.up, out RaycastHit hit, headroom))
            {
                // �stte engel varsa tekrar e�ilmeye zorla
                isCrouching = true;
            }
        }
    }
    





}
