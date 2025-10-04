using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [Header("Tuning")]
    [SerializeField] private float pickupDistance = 3f;
    [SerializeField] private Transform holdPoint;

    [Header("Debug")]
    [SerializeField] private bool showDebugRay = true;   // ýþýný çiz / çizme

    private Camera cam;
    private Rigidbody carriedRb;
    private PlayerInput input;

    private void Awake()
    {
        cam = Camera.main;
        input = GetComponent<PlayerInput>();
        input.actions["Interact"].performed += _ => Interact();
    }

    private void Update()
    {
        // --- DEBUG RAY --------------------------------------------------------
        if (showDebugRay)
        {
            Debug.DrawRay(cam.transform.position,cam.transform.forward * pickupDistance,Color.red);// Scene view’da görünür
        }
    }

    private void Interact()
    {
        if (carriedRb == null) TryPick();
        else Drop();
    }

    private void TryPick()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance) &&
            hit.transform.CompareTag("Pickable"))
        {
            carriedRb = hit.rigidbody;
            carriedRb.isKinematic = true;

            carriedRb.transform.SetParent(holdPoint);
            carriedRb.transform.localPosition = Vector3.zero;
            carriedRb.transform.localRotation = Quaternion.identity;
        }
    }

    private void Drop()
    {
        carriedRb.transform.SetParent(null);
        carriedRb.isKinematic = false;
        carriedRb = null;
    }
}
