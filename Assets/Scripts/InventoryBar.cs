using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour
{
    public Transform slotContainer;
    public GameObject slotPrefab;
    public int slotCount = 4;
    public int selectedIndex = 0;

    private InventoryManager _inventoryManager;
    private GameObject[] slots;

    private void Awake()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();
        slots = new GameObject[slotCount];

        // slotlarý dinamik oluþtur
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            slots[i] = slot;
        }
    }

    private void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll > 0f) // yukarý
        {
            selectedIndex = (selectedIndex - 1 + slotCount) % slotCount;
            HighlightSlot(selectedIndex);
        }
        else if (scroll < 0f) // aþaðý
        {
            selectedIndex = (selectedIndex + 1) % slotCount;
            HighlightSlot(selectedIndex);
        }
    }

    public void UpdateInventoryUI()
    {
        if (_inventoryManager == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            Image iconImage = slots[i].transform.Find("Icon").GetComponent<Image>();
            TMP_Text nameText = slots[i].transform.Find("Name").GetComponent<TMP_Text>();
            Image highlight = slots[i].transform.Find("Highlight").GetComponent<Image>();

            ItemData item = _inventoryManager.items[i];

            if (item != null)
            {
                iconImage.sprite = item.icon;
                iconImage.enabled = true;
                nameText.text = item.itemName;
            }
            else
            {
                iconImage.sprite = null;
                iconImage.enabled = false;
                nameText.text = "";
            }

            highlight.enabled = (i == selectedIndex);
        }
    }

    public void HighlightSlot(int index)
    {
        selectedIndex = index;
        UpdateInventoryUI();
    }
}
