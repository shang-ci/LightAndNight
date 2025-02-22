// 文件：ScriptableObjects/CardGrowthConfig.cs
using UnityEngine;

public enum ExperienceCurve { Linear, Quadratic }

[CreateAssetMenu(menuName = "Card/Growth Config")]

public class CardExperience : ScriptableObject
{
    public int level;
    public int requiredExp;

    public int baseExp;
    public int expPerLevel;
    public ExperienceCurve curveType;

    private int GetRequiredExp(int targetLevel)
    {
        var expData = ExistingExcelHelper.cardExperienceList
            .Find(e => e.level == targetLevel);

        switch (expData.curveType)
        {
            case ExperienceCurve.Linear:
                return expData.baseExp + expData.expPerLevel * (targetLevel - 1);
            case ExperienceCurve.Quadratic:
                return expData.baseExp + expData.expPerLevel * (targetLevel - 1) * targetLevel / 2;
        }
        return 0;
    }

}

