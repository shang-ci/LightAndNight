using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour
{
    public GridLayoutGroup inventoryGrid;
    public ItemSlot[] itemSlots;
    
    public void InitializeInventory(Item[] items)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < items.Length)
                itemSlots[i].SetItem(items[i]);
            else
                itemSlots[i].ClearItem();
        }
    }
}

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Text countText;
    public Item item;
    
    public void SetItem(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.icon;
        countText.text = newItem.count.ToString();
    }
    
    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
        countText.text = "";
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // 处理物品使用逻辑
    }
}

public class Item
{
    public Sprite icon;
    public string name;
    public int count;
}