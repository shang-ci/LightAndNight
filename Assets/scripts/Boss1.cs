using UnityEngine;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    public string bossName; // Boss 名称
    public int hp;          // Boss 血量

    public int maxHp = 100; // 最大血量

    public Slider healthBar; // 血条 UI

    public int health = 100; // 生命值
    public bool isDead; // 是否死亡

    private ExperienceRewardManager experienceRewardManager; // 新增引用
    private void Start()
    {
        experienceRewardManager = Object.FindFirstObjectByType<ExperienceRewardManager>(); // 查找 ExperienceRewardManager 脚本实例

        if (experienceRewardManager == null)
        {
            Debug.LogError("ExperienceRewardManager not found in the scene!");
        }
        
        if (health <= 0)
        {
            Die();
        }

        hp = 0;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHp;
            healthBar.value = hp; // 初始化血条
        }
    }

    void Update()
    {
        // 假设 Boss 的生命值为 0 时死亡
        if (health <= 0)
        {
            Die();

            Debug.Log("已击败boss 可获得经验值");
        }
    }

    //新添加class控制多个角色的经验值获得

    void Die()
    {
        isDead = true;
        // 增加经验值
        //判断是否都进行攻击，如果都进行攻击则增加经验值
        // MenuManager.Instance.AddExperienceToPlayer1(10);

        // MenuManager.Instance.AddExperienceToPlayer2(5);

        experienceRewardManager.RewardExperience(10, 5); // 为两个玩家分配经验

        Debug.Log("Osborn gain 5 exp.");

        Debug.Log("Evan gain 10 exp.");
        // 销毁 Boss 对象
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Max(0, maxHp); // 血量不低于0

        // 更新血条
        if (healthBar != null)
        {
            healthBar.value = hp;
        }

        Debug.Log($"{bossName} 受到 {damage} 点伤害，剩余血量: {hp}");

        // if (hp <= 0)
        // {
        //     Debug.Log($"{bossName} 被击败！");
        // }
    }

    public void TakeDamage1(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            isDead = true;
            //Debug.Log($"{bossName} is dead.");
        }
    }
    public void BossDefeated()
    {
        MenuManager.Instance.AddExperienceToPlayer1(10);

        MenuManager.Instance.AddExperienceToPlayer2(5);

        Destroy(gameObject); // 或其他处理
    }



    // public void TakeDamage1(int damage)
    // {
    //     hp -= damage;
    //     hp = Mathf.Max(0, hp); // 确保血量不为负数
    //     Debug.Log($"{bossName} 受到了 {damage} 点伤害，剩余生命值：{hp}");

    //     if (hp <= 0)
    //     {
    //         Debug.Log($"{bossName} 被击败了！");
    //     }
    // }
}