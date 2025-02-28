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
    private void Start()
    {
        hp = 80;
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

    void Die()
    {
        // 增加经验值
        MenuManager.Instance.AddExperience(10);

        Debug.Log("You gain 10 exp.");
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
        MenuManager.Instance.AddExperience(10);
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