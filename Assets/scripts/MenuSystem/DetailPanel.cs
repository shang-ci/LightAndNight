using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailPanel : MonoBehaviour
{
    public List<ItemSO> items_Card;
    public List<ItemSO> items_Skill;
    public List<ItemSO> items_Inventory;

    public Transform cardInitParent;
    public Transform skillInitParent;
    public Transform inventoryInitParent;

    public CardInit cardInitPrafb;
    // public SkillInit skillInitPrafb;
    // public InventoryInit inventoryInitPrafb;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SetCardDetail();
            Debug.Log("C key is pressed");
        }
    }

    public void SetCardDetail()
    {
        foreach(var cardinit in cardInitParent.GetComponentsInChildren<CardInit>())
        {
            Destroy(cardinit.gameObject);
        }

        foreach (var itemCard in items_Card)
        {
            var cardInit = Instantiate(cardInitPrafb, cardInitParent);
            cardInit.SetCardDetail(itemCard);
            Debug.Log("SetCardDetail");
        }
    }   
}
