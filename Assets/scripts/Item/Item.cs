using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Item_Card", menuName = "Item/Card")]
public class Item : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite itemIcon;
    public ItemType itemType;
    [TextArea(10, 15)]
    public string itemDescription;
}
