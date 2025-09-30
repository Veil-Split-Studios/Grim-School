using UnityEngine;

public class Item
{
    public string name;
    public string description;
    public UnityEngine.Sprite icon;
    public bool isUsable;
    public int durability;

    public Item(string name, string description = "", Sprite icon = null, bool isUsable = false, int durability = 0)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.isUsable = isUsable;
        this.durability = durability;
    }
}
