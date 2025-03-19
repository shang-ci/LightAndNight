using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInit : MonoBehaviour
{ 
    public ItemSO itemCard;
    public Text des;
    public Image icon;

    public void SetCardDetail(ItemSO item)
    {
        itemCard = item;
        des.text = item.itemDescription;
        icon.sprite = item.itemIcon;
    }
}
