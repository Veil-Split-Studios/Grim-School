using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject _handPoint;
    [SerializeField] private InventoryManager _invMan;
    [SerializeField] private InventoryBar _invBar;
    private int selectedIndex;

    public void OnDropItem()
    {
        selectedIndex = _invBar.selectedIndex;
        TryDrop();
    }

    private void TryDrop()
    {
        if (_handPoint.transform.childCount == 0 || _invMan.items[selectedIndex] == null) return;
        GameObject heldItem = _handPoint.transform.GetChild(0).gameObject;
        heldItem.transform.SetParent(null);
        Vector3 offset = new Vector3(0, 0.3f, 0);
        heldItem.transform.position = _handPoint.transform.position + offset;

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.useGravity = true;
            rb.AddForce(transform.forward * 1f, ForceMode.Impulse);
        }
        _invMan.items[selectedIndex] = null;
        _invBar.UpdateInventoryUI();
    }

    private void Start()
    {
        if (_invBar != null)
        {
            selectedIndex = _invBar.selectedIndex;
        }
    }
}
