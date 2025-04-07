using System.Collections.Generic;
using UnityEngine;

//要是需要仿照崩坏，那每个角色都有一个装备栏，装备栏里有多个装备项，同理每个角色也有自己的卡牌管理器，卡牌库来管理
public class EquipManager : MonoBehaviour
{
    public static EquipManager instance;
    public Transform equipmentParent;
    public Equipment_Item equipmentItemPrefab;
    public List<Item> equipmentItems = new List<Item>();



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

    public void CreatItem(Item item)
    {
        equipmentItems.Add(item);
        AddEquipmentItem(item);
    }

    public void AddEquipmentItem(Item item)
    {
        Equipment_Item newItem = Instantiate(equipmentItemPrefab, equipmentParent);

        newItem.SetEquipmentItem(item);
    }

    public void text()
    {
        if (equipmentItems.Count > 0) // 确保列表中有装备项
        {
            int randomIndex = Random.Range(0, equipmentItems.Count); // 随机选择一个索引
            AddEquipmentItem(equipmentItems[randomIndex]); // 添加随机选择的装备项
        }
        else
        {
            Debug.LogWarning("equipmentItems 列表为空，无法添加装备项！");
        }
    }
}
