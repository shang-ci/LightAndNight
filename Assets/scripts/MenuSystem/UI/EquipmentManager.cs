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

}
