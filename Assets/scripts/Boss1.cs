using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Boss1 : MonoBehaviour
{
    public string bossName; // Boss 名称
    public int hp;          // Boss 血量

    public int maxHp = 100; // 最大血量

    public Slider healthBar; // 血条 UI

    public int health = 100; // 生命值
    public bool isDead; // 是否死亡

    public int[] playerExp = new int[2];

    private ExperienceRewardManager experienceRewardManager; // 新增引用

    private float attackInterval = 2.0f; // 自动攻击间隔时间
    private float nextAttackTime = 0f; // 下次攻击时间

    private void Start()
    {
        experienceRewardManager = Object.FindFirstObjectByType<ExperienceRewardManager>(); // 查找 ExperienceRewardManager 脚本实例

        if (experienceRewardManager == null)
        {
            Debug.LogError("ExperienceRewardManager not found in the scene!");
        }

        // 确保 health 和 hp 的初始值正确
        health = maxHp;
        hp = maxHp;

        if (health <= 0)
        {
            Die();
        }

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

        // 自动攻击逻辑
        if (Time.time >= nextAttackTime)
        {
            //AutoAttack();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void Die()
    {
        isDead = true;
        // 从GameManager的bosses数组中移除自己
        var list = new List<Boss1>(GameManager.Instance.bosses);
        list.Remove(this);
        GameManager.Instance.bosses = list.ToArray();
        Destroy(gameObject, 1f); // 延迟1秒销毁
        experienceRewardManager.RewardExperience(playerExp); // 为两个玩家分配经验
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
}