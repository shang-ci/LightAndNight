using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInit : MonoBehaviour
{ 
    public Item itemCard;
    public Text des;
    public Image icon;

    public void SetCardDetail(Item item)
    {
        itemCard = item;
        des.text = item.itemDescription;
        icon.sprite = item.itemIcon;
    }
}
