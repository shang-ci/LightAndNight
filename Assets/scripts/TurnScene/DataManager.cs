using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 单例实例
    public static DataManager Instance { get; private set; }

    // 玩家数据类
    [System.Serializable]
    public class PlayerData
    {
        public string playerName = "Player";
        public int experience;
        public int level = 1;
    }

    // 双玩家数据
    public PlayerData[] players = new PlayerData[2];

    private void Awake()
    {
        // 初始化玩家数据
        for(int i = 0; i < players.Length; i++)
        {
            players[i] = new PlayerData();
        }

        // 单例控制
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

    // 保存经验值（示例方法）
    public void AddExperience(int playerIndex, int amount)
    {
        players[playerIndex].experience += amount;
        CheckLevelUp(playerIndex);
    }

    // 等级提升逻辑
    private void CheckLevelUp(int playerIndex)
    {
        // 假设每1000经验升1级
        while(players[playerIndex].experience >= 5)
        {
            players[playerIndex].experience -= 5;
            players[playerIndex].level++;
        }
    }
}