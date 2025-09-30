using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public bool isUsable;
    public int durability;

    public Item GetItem()
    {
        return new Item(itemName, description, icon, isUsable, durability);
    }
}