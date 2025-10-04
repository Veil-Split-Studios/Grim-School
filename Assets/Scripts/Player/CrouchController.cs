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

    [Header("Boy ve Kamera Yüksekliði")]
    [SerializeField] private float standHeight = 1.8f;
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private float standCameraY = 1.6f;
    [SerializeField] private float crouchCameraY = 1.0f;

    [Header("Yumuþak Geçiþ")]
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
        //"Crouch" InputAction'ý bul
        crouchAction = playerInput.actions.FindAction("Crouch", true);

        //Tuþa basýnca crouch aç
        crouchAction.started += ctx => isCrouching = true;
        //Tuþ býrakýlýnca crouch kapat
        crouchAction.canceled += ctx => isCrouching = false;
    }
    void Update()
    {
        // Hedef deðerler
        float targetHeight = isCrouching ? crouchHeight : standHeight;
        float targetCameraY = isCrouching ? crouchCameraY : standCameraY;
        float targetModelY = isCrouching
            ? modelStartLocalPos.y - (standHeight - crouchHeight) / 2f
            : modelStartLocalPos.y;

        // Sadece height'ý deðiþtir, center sabit kalsýn!
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
        // Sadece ayaða kalkarken kontrol et
        if (!isCrouching)
        {
            float headroom = standHeight - crouchHeight + 0.1f;
            Vector3 origin = transform.position + Vector3.up * (crouchHeight / 2f);
            if (Physics.SphereCast(origin, characterController.radius * 0.95f, Vector3.up, out RaycastHit hit, headroom))
            {
                // Üstte engel varsa tekrar eðilmeye zorla
                isCrouching = true;
            }
        }
    }
    





}
