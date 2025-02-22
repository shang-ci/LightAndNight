// 文件：ScriptableObjects/CardGrowthConfig.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Growth Config")]

public class CardGrowthConfig : ScriptableObject
{
    public string cardId;
    public int level;
    public float[] attributeMults; // 六个属性的倍数数组
}

// public class CardGrowthConfig : ScriptableObject
// {
//     [Header("基础信息")] 
//     public string cardId; // 卡牌唯一标识
    
//     [Header("等级配置")]
//     public LevelData[] levelData;
    
//     [System.Serializable]
//     public class LevelData
//     {
//         [Tooltip("升到该等级需要的总经验")]
//         public int requiredExp;
        
//         [Tooltip("六个属性的倍率数组（顺序需与基础属性一致）")]
//         public float[] attributeMultipliers;
//     }
// }