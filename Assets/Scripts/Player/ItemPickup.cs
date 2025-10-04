using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private Transform handPoint;
    private Collider cl;
    private Rigidbody rb;
    private bool isPickedUp = false;

    private InventoryManager inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<InventoryManager>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    /// <summary>
    /// Input System callback – Interact action ile eþleþtir
    /// </summary>
    public void OnInteract()
    {
        TryPickup();
    }

    private void TryPickup()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.green, 0.5f);

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            PickableItem pickable = hit.collider.GetComponent<PickableItem>();
            if (pickable != null)
            {
                pickable.PickUp(inventory);
                rb = hit.rigidbody.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = false;
                    rb.detectCollisions = false;
                }
                // Sahnede PickableItem var mý kontrol et
                cl = hit.collider;
                cl.transform.SetParent(handPoint);
                cl.transform.localPosition = Vector3.zero;
                cl.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
