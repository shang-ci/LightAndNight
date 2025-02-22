using UnityEngine;

[System.Serializable]
public class Card : MonoBehaviour
{
    public string cardName; // 卡牌名称
    public int damage;      // 伤害值
    public Sprite cardSprite; // 卡牌图片

     public string id; // 对应Excel中的卡牌ID
    public int currentLevel = 1;
    public int currentExp = 0;
    public float[] attributes = new float[6]; // 六个属性值

    // 经验增长处理
    public void GainExperience(int exp)
    {
        currentExp += exp;
        CheckUpgrade();
    }

    private void CheckUpgrade()
    {
        if (currentExp < GetRequiredExp(currentLevel + 1)) return;

        // 升级处理
        currentLevel++;
        currentExp -= GetRequiredExp(currentLevel); 

        // 应用属性倍数
        var levelData = GetLevelData(currentLevel);
        ApplyAttributes(levelData);

        // 通知UI更新
        //AttributePanel.Instance.UpdateCard(this);
    }

    // 获取升级所需经验
    private int GetRequiredExp(int targetLevel)
    {
        return ExistingExcelHelper.cardExperienceList
            .Find(e => e.level == targetLevel).requiredExp;
    }

    // 获取当前等级的属性倍数
    private CardGrowthConfig GetLevelData(int level)
    {
        return ExistingExcelHelper.cardLevelDataList
            .Find(ld => ld.cardId == id && ld.level == level);
    }

    // 应用属性倍数
    private void ApplyAttributes(CardGrowthConfig data)
    {
        for (int i = 0; i < 6; i++)
        {
            attributes[i] *= data.attributeMults[i];
        }
    }
}

