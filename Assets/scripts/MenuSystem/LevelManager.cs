using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // 设定玩家1和玩家2的经验值和等级
    public int player1Level;

    public int player2Level;

    public int expperlevel1 = 5; // 每升一级所需经验值

    public int expperlevel2 = 5; // 每升一级所需经验值

    private ExperienceRewardManager experienceRewardManager; // 引用经验管理器

    void Start()
    {
        experienceRewardManager = FindFirstObjectByType<ExperienceRewardManager>(); // 获取经验奖励管理器实例

        if (experienceRewardManager == null)
        {
            Debug.LogError("ExperienceRewardManager not found in the scene!");
        }

        Debug.Log($"plaier1的等级:{player1Level}");
    }

    void Update()
    {
        // 检查玩家的经验
        player1Level = GetLevel1Experience(MenuManager.Instance.player1Experience, player1Level, expperlevel1);

        player2Level = GetLevel2Experience(MenuManager.Instance.player2Experience, player2Level, expperlevel2);
    }

    public int GetLevel1Experience(int playerExperience, int playerLevel, int expPerLevel)
    {
        //Debug.Log($"GetLevel1Experience called with playerExperience: {playerExperience}, playerLevel: {playerLevel}, expPerLevel: {expPerLevel}");
        
        // 如果经验值达到升级条件
        //Debug.Log($"Checking if playerExperience >= playerLevel * expPerLevel: {playerExperience} >= {playerLevel * expPerLevel}");
        if (playerExperience >= playerLevel * expPerLevel)
        {
            //Debug.Log("Condition met, calculating new level.");
            // 计算玩家的最新等级
            int newLevel = playerExperience / expPerLevel;
            //Debug.Log($"取得player1的等级:{newLevel}");
            if (newLevel > playerLevel)
            {
                playerLevel = newLevel; // 更新玩家等级
                Debug.Log($"玩家升级！新的等级: {playerLevel}");
            }
        }
        else
        {
            Debug.Log("Condition not met.");
        }
        return playerLevel;
    }

    public int GetLevel2Experience(int playerExperience, int playerLevel, int expPerLevel)
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
        return playerLevel;
    }
}