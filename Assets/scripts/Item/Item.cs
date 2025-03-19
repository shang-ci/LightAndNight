using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item_Card", menuName = "Item/Card")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int itemID;
    public string itemDescription;
    public Sprite itemIcon;
}
