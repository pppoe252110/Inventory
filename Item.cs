using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class Item : ScriptableObject
{
    public string Name = "Item";
    public Sprite Icon;
    public GameObject Prefab;
}
