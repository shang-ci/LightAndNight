using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Card[] cards; // 卡牌数组
    public CardUI[] cardUIs; // 卡牌 UI 数组
    public Boss1[] bosses; // Boss 数组
    public bool isAutoMode = false; // 是否为自动模式

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int _experience; // 序列化字段用于编辑器调试

    // 公开属性（实际使用时建议用方法控制）
    public int CurrentExperience => _experience;

    public CardData[] cards1;
    // private void Start()
    // {
    //     InitializeCards();
    //     if (isAutoMode)
    //     {
    //         StartCoroutine(AutoAttack());
    //     }
    //     // 打印所有卡牌的名称和伤害值
    //     foreach (var card in cards1)
    //     {
    //         Debug.Log($"卡牌名称: {card.cardName}, 伤害值: {card.damage}");
    //     }
    // }

    void Start()
    {
        Time.timeScale = 1; // 确保时间缩放比例为 1
        InitializeCards();
        isAutoMode = true; // 确保自动模式为 true
        if (isAutoMode)
        {
            //Debug.Log("Starting AutoAttack coroutine."); // 添加日志检查
            StartCoroutine(AutoAttack());
        }
        // 打印所有卡牌的名称和伤害值
        foreach (var card in cards1)
        {
            Debug.Log($"卡牌名称: {card.cardName}, 伤害值: {card.damage}");
        }
    }

    private void InitializeCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cardUIs[i].Initialize(cards[i]);
        }
    }

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

    public void AddExperience(int amount)
    {
        _experience += amount;
        Debug.Log($"获得经验 {amount}，当前总经验：{_experience}");
    }

    // 完全重置游戏时调用
    public void ResetProgress()
    {
        _experience = 0;
    }

    private IEnumerator AutoAttack()
    {
        Debug.Log("AutoAttack started.");
        while (true)
        {
            yield return new WaitForSeconds(2f); // 每 2 秒攻击一次

            // 检查是否所有 Boss 都已死
            bool allBossesDead = true;
            foreach (var boss in bosses)
            {
                if (!boss.isDead)
                {
                    allBossesDead = false;
                    break;
                }
            }

            if (allBossesDead)
            {
                //Debug.Log("All bosses are dead. Stopping AutoAttack.");
                yield break; // 退出协程
            }

            // 随机选择一张卡牌和一个 Boss
            CardUI selectedCard = cardUIs[Random.Range(0, cardUIs.Length)];
            Boss1 selectedBoss = bosses[Random.Range(0, bosses.Length)];
            //auto attack出牌逻辑在此处改（i.e. 选择伤害最高的牌）

            // 如果选中的 Boss 已死，跳过这次攻击
            if (selectedBoss.isDead)
            {
                continue;
            }

            // 攻击 Boss
            selectedBoss.TakeDamage1(selectedCard.card.damage);
            //Debug.Log($"自动使用了 {selectedCard.card.cardName} 对 {selectedBoss.bossName} 造成了 {selectedCard.card.damage} 点伤害。");

            // Debug.Log("gained 10 experiences.");

            // Debug.Log("1f属性 -> 1.2f属性");
        }
    }

}