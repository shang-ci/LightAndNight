using UnityEngine;
using UnityEngine.UI;

public class Equipment_Item : MonoBehaviour
{
    public Image icon;
    public ItemSO item;
    private int id;


    public void SetEquipmentItem(ItemSO item)
    {
        this.item = item;
        this.id = item.itemID;
        icon.sprite = item.itemIcon;
    }
}
