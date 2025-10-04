using Unity.VisualScripting;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [Header("Item Data Reference")]
    public ItemData itemData;   // Bu objenin temsil ettiði ScriptableObject

    //[Header("Settings")]
    //public bool destroyOnPickUp = true; // Alýndýktan sonra objeyi sahneden sil

    // Oyuncu bu objeyi aldýðýnda çaðrýlacak fonksiyon
    public void PickUp(InventoryManager inventory)
    {
        if (itemData == null)
        {
            Debug.LogWarning($"{gameObject.name} has no ItemData assigned!");
            return;
        }

        inventory.AddItem(itemData);

        //if (destroyOnPickUp)
        //{
        //    Destroy(gameObject);
        //}
    }
}