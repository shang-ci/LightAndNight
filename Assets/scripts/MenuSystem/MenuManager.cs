using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance; // 单例

    private int experience = 0;

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

    // 增加经验值的方法
    public void AddExperience(int amount)
    {
        experience += amount;
        Debug.Log("当前经验值: " + experience);
    }

    // 获取当前经验值
    public int GetExperience()
    {
        return experience;
    }
}