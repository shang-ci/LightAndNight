/* 
功能:
管理玩家的经验值和等级。

挂载对象:
应该挂载在一个全局管理对象上，例如 GameManager。

重要变量:
playerExperience: 存储每个玩家的经验值。
playerLevel: 存储每个玩家的等级。
expperlevel: 存储每个等级所需的经验值。
 */

using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public int[] playerExperience = new int[100];
    public int[] playerLevel = new int[100];

    public int[] expperlevel = new int[100];

    public string[] playerName = new string[] { "Osborn", "Evan" };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddExperienceToPlayer(int playerIndex, int amount)
    {
        if (playerIndex < 0 || playerIndex >= playerExperience.Length)
        {
            Debug.LogError("Invalid player index");
            return;
        }
        playerExperience[playerIndex] += amount;
        Debug.Log($"Player {playerIndex + 1} 当前经验值: " + playerExperience[playerIndex]);
    }

    public int GetPlayerExperience(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= playerExperience.Length)
        {
            Debug.LogError("Invalid player index");
            return 0;
        }
        return playerExperience[playerIndex];
    }
}