using UnityEngine;

public class ExperienceRewardManager : MonoBehaviour
{
     private LevelManager levelManager; // 引用 LevelManager

     void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>(); // 获取 LevelManager 实例

        // if (levelManager == null)
        // {
        //     Debug.LogError("LevelManager not found in the scene!");
        // }
    }

    // 通过玩家实例分配经验
    public void RewardExperience(int player1Exp, int player2Exp)
    {
        // 通过 MenuManager 实例分配经验值
        MenuManager.Instance.AddExperienceToPlayer1(player1Exp);
        MenuManager.Instance.AddExperienceToPlayer2(player2Exp);

        Debug.Log($"Player 1 获得 {player1Exp} 点经验");
        Debug.Log($"Player 2 获得 {player2Exp} 点经验");

        //levelManager.Update(); // 检查并更新等级
    }
}