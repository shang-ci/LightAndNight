using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // public static MenuManager player1;

    // public static MenuManager player2;

    // private int experience = 0;

    public static MenuManager Instance;

    public int player1Experience = 0;
    public int player2Experience = 0;

//     void Awake()
// {
//     if (player1 == null)
//     {
//         player1 = this;
//         DontDestroyOnLoad(gameObject); // 跨场景保留
//     }
//     else if (player2 == null)
//     {
//         player2 = this; // 这里确保 player2 是另一个实例
//         DontDestroyOnLoad(gameObject); // 跨场景保留
//     }
//     else
//     {
//         Destroy(gameObject);
//     }
//}

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

    public void AddExperienceToPlayer1(int amount)
    {
        player1Experience += amount;
        Debug.Log("Player 1 当前经验值: " + player1Experience);
    }

    public void AddExperienceToPlayer2(int amount)
    {
        player2Experience += amount;
        Debug.Log("Player 2 当前经验值: " + player2Experience);
    }

    public int GetPlayer1Experience()
    {
        return player1Experience;
    }

    public int GetPlayer2Experience()
    {
        return player2Experience;
    }

    // 增加经验值的方法
    // public void AddExperience(int amount)
    // {
    //     experience += amount;
    //     Debug.Log("当前经验值: " + experience);
    // }

    // // 获取当前经验值
    // public int GetExperience()
    // {
    //     return experience;
    // }
}