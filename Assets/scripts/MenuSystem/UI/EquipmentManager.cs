using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager instance;
    public Transform equipmentParent;
    public Equipment_Item equipmentItemPrefab;
    public List<ItemSO> equipmentItems = new List<ItemSO>();



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            text();
        }
    }

    public void CreatItem(ItemSO item)
    {
        equipmentItems.Add(item);
        AddEquipmentItem(item);
    } 
    
    public void AddEquipmentItem(ItemSO item)
    {
        Equipment_Item newItem = Instantiate(equipmentItemPrefab, equipmentParent);

        newItem.SetEquipmentItem(item);
    }

    public void text()
    {
        AddEquipmentItem(equipmentItems[0]);
    }

}
