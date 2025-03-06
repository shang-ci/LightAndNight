using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // 设定玩家1和玩家2的经验值和等级
    public int player1Level = 200;

    public int player2Level = 0;

    public const int EXP_PER_LEVEL1 = 5; // 每升一级所需经验值

    public const int EXP_PER_LEVEL2 = 5; // 每升一级所需经验值

    private ExperienceRewardManager experienceRewardManager; // 引用经验管理器

    void Start()
    {
        experienceRewardManager = FindFirstObjectByType<ExperienceRewardManager>(); // 获取经验奖励管理器实例

        if (experienceRewardManager == null)
        {
            Debug.LogError("ExperienceRewardManager not found in the scene!");
        }
    }

    void Update()
    {
        // 检查玩家的经验
        GetLevel1Experience(ref MenuManager.Instance.player1Experience, ref player1Level, EXP_PER_LEVEL1);

        GetLevel2Experience(ref MenuManager.Instance.player2Experience, ref player2Level, EXP_PER_LEVEL2);
    }

    public void GetLevel1Experience(ref int playerExperience, ref int playerLevel, int expPerLevel)
    {
        // 如果经验值达到升级条件
        if (playerExperience >= playerLevel * expPerLevel)
        {
            // 计算玩家的最新等级
            int newLevel = playerExperience / expPerLevel;
            Debug.Log($"取得player1的等级:{newLevel}");
            if (newLevel > playerLevel)
            {
                playerLevel = newLevel; // 更新玩家等级
                Debug.Log($"玩家升级！新的等级: {playerLevel}");
            }
        }
    }

    public void GetLevel2Experience(ref int playerExperience, ref int playerLevel, int expPerLevel)
    {
        // 如果经验值达到升级条件
        if (playerExperience >= playerLevel * expPerLevel)
        {
            // 计算玩家的最新等级
            int newLevel = playerExperience / expPerLevel;
            if (newLevel > playerLevel)
            {
                playerLevel = newLevel; // 更新玩家等级
                Debug.Log($"玩家升级！新的等级: {playerLevel}");
            }
        }
    }
}