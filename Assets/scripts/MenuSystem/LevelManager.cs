/* 
功能:
管理玩家的等级，根据经验值计算等级。

挂载对象:
应该挂载在一个全局管理对象上，例如 GameManager。

重要变量:
player1Level, player2Level: 玩家1和玩家2的等级。
experienceRewardManager: 引用经验奖励管理器的实例。
 */

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int player1Level;
    public int player2Level;
    private ExperienceRewardManager experienceRewardManager; // 引用经验管理器

void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        experienceRewardManager = FindFirstObjectByType<ExperienceRewardManager>(); // 获取经验奖励管理器实例

        if (experienceRewardManager == null)
        {
            Debug.LogError("ExperienceRewardManager not found in the scene!");
        }

        //Debug.Log($"plaier1的等级:{player1Level}");
    }

    void Update()
    {
        // 检查玩家的经验
        player1Level = GetLevelExperience(MenuManager.Instance.playerExperience[0],MenuManager.Instance.playerLevel[0],MenuManager.Instance.expperlevel[0]);

        player2Level = GetLevelExperience(MenuManager.Instance.playerExperience[1],MenuManager.Instance.playerLevel[1],MenuManager.Instance.expperlevel[1]);

        // Debug.Log($"player1的等级:{player1Level}");
        // Debug.Log($"player2的等级:{player2Level}");
        //数值正确
    }

    public int GetLevelExperience(int playerExperience, int playerLevel, int expPerLevel)
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
                //Debug.Log($"玩家升级！新的等级: {playerLevel}");
            }
        }
        else
        {
            Debug.Log("Condition not met.");
        }
        return playerLevel;
    }

    // public int GetLevel2Experience(int playerExperience, int playerLevel, int expPerLevel)
    // {
    //     // 如果经验值达到升级条件
    //     if (playerExperience >= playerLevel * expPerLevel)
    //     {
    //         // 计算玩家的最新等级
    //         int newLevel = playerExperience / expPerLevel;
    //         if (newLevel > playerLevel)
    //         {
    //             playerLevel = newLevel; // 更新玩家等级
    //             Debug.Log($"玩家升级！新的等级: {playerLevel}");
    //         }
    //     }
    //     return playerLevel;
    // }
}