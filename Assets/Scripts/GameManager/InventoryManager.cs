using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    public ItemData[] items;         // ScriptableObject item’leri tutacak
    [SerializeField] private int slotCount = 4;

    private InventoryBar _inventoryBar;
    public int selectedIndex;

    private void Awake()
    {
        items = new ItemData[slotCount];          // slot sayýsý kadar dizi aç
        _inventoryBar = FindObjectOfType<InventoryBar>();
    }

    /// <summary>
    /// Yeni bir item ekler (ilk boþ slota koyar).
    /// </summary>
    public void AddItem(ItemData newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                break; // Sadece ilk boþ slota ekle
            }
        }

        if (_inventoryBar != null)
        {
            _inventoryBar.UpdateInventoryUI();
        }
    }

    /// <summary>
    /// Seçili slotu deðiþtir.
    /// </summary>
    public void SetSelectedSlot(int index)
    {
        if (index >= 0 && index < slotCount)
        {
            selectedIndex = index;

            if (_inventoryBar != null)
            {
                _inventoryBar.HighlightSlot(selectedIndex);
            }
        }
    }

    private void Start()
    {
        if (_inventoryBar != null)
        {
            selectedIndex = _inventoryBar.selectedIndex;
        }
    }
}
