using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    [TextArea] public string description;

    [Header("Visuals")]
    public Sprite icon;             // UI�de g�stermek i�in
    public GameObject prefab;       // Sahneye instantiate etmek istersen

    [Header("Properties")]
    public bool isUsable;
    public int durability;
}