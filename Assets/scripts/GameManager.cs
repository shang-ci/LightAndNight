using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Card[] cards; // 卡牌数组
    public CardUI[] cardUIs; // 卡牌 UI 数组
    public Boss1[] bosses; // Boss 数组
    public bool isAutoMode = false; // 是否为自动模式

    public static GameManager Instance; // 单例模式实例

    [SerializeField]
    private int _experience; // 序列化字段用于编辑器调试

    public int CurrentExperience => _experience;

    [SerializeField] public Button startButton; // 自动攻击按钮

    public CardData[] cards1;

    void Start()
    {
        Time.timeScale = 1; // 确保时间缩放比例为 1
        InitializeCards(); // 初始化卡牌
    }

    public void StartAutoAttack()
    {
        //Debug.Log("[调试] 按钮点击事件触发"); // 检查是否收到点击
        isAutoMode = true;

        // 禁用按钮并修改文本
        if (startButton != null)
        {
            startButton.interactable = false; // 禁用按钮交互
            startButton.GetComponentInChildren<TextMeshProUGUI>().text = "battle now..."; // 修改按钮文本
        }

        StartCoroutine(AutoAttack()); // 启动自动攻击协程
    }

    private void InitializeCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cardUIs[i].Initialize(cards[i]); // 初始化每张卡牌的UI
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ResetProgress()
    {
        _experience = 0; // 重置经验值
    }

    private IEnumerator AutoAttack()
    {
        //Debug.Log("[调试] 自动攻击协程启动"); // 检查协程何时启动
        while (isAutoMode) // 用标志控制循环
        {
            yield return new WaitForSeconds(0.5f); // 每 0.5 秒执行一次

            // 检查Boss数组有效性
            if (bosses == null || bosses.Length == 0)
            {
                Debug.LogWarning("No bosses available"); // 警告没有可用的Boss
                break; // 退出循环
            }

            // 过滤已死亡的Boss
            var aliveBosses = bosses.Where(b => b != null && !b.isDead).ToArray();
            if (aliveBosses.Length == 0)
            {
                Debug.Log("All bosses dead"); // 所有Boss已死亡
                break; // 退出循环
            }

            // 随机选择存活的Boss
            Boss1 selectedBoss = aliveBosses[Random.Range(0, aliveBosses.Length)];

            // 选择卡牌逻辑（示例随机选择）
            CardUI selectedCard = cardUIs[Random.Range(0, cardUIs.Length)];

            // 执行攻击
            selectedBoss.TakeDamage(selectedCard.card.damage);
            Debug.Log($"Auto attack: {selectedCard.card.cardName} -> {selectedBoss.bossName}"); // 输出攻击日志
        }

        // 攻击结束后恢复按钮状态
        isAutoMode = false;
        if (startButton != null)
        {
            startButton.interactable = true; // 恢复按钮交互
            startButton.GetComponentInChildren<TextMeshProUGUI>().text = "auto attack"; // 恢复按钮文本
        }

        Debug.Log("[调试] 自动攻击结束，按钮已恢复");
    }
}


// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using System.Linq;
// using TMPro;

// public class GameManager : MonoBehaviour
// {
//     public Card[] cards; // 卡牌数组
//     public CardUI[] cardUIs; // 卡牌 UI 数组
//     public Boss1[] bosses; // Boss 数组
//     public bool isAutoMode = false; // 是否为自动模式

//     public static GameManager Instance; // 单例模式实例

//     public static GameManager Instance1; // 备用单例模式实例

//     [SerializeField]
//     private int _experience; // 序列化字段用于编辑器调试

//     // 公开属性（实际使用时建议用方法控制）
//     public int CurrentExperience => _experience;

//     [SerializeField] public Button startButton; // 拖拽按钮到Inspector

//     public CardData[] cards1;

//     // 初始化方法
//     void Start()
//     {
//         Time.timeScale = 1; // 确保时间缩放比例为 1
//         InitializeCards(); // 初始化卡牌
//         // isAutoMode = true; // 确保自动模式为 true
//         // if (isAutoMode)
//         // {
//         //     //Debug.Log("Starting AutoAttack coroutine."); // 添加日志检查
//         //     StartCoroutine(AutoAttack());
//         // }
//         // // 打印所有卡牌的名称和伤害值
//         // foreach (var card in cards1)
//         // {
//         //     Debug.Log($"卡牌名称: {card.cardName}, 伤害值: {card.damage}");
//         // }
//     }

//     // 新增方法：由按钮触发自动攻击
//     public void StartAutoAttack()
//     {
//         Debug.Log("[调试] 按钮点击事件触发"); // 检查是否收到点击
//         isAutoMode = true;
//         StartCoroutine(AutoAttack()); // 启动自动攻击协程

//         // 可选：禁用按钮防止重复点击
//         // startButton.interactable = false;

//         // 禁用按钮
//         if (startButton != null)
//         {
//             startButton.interactable = false; // 禁用按钮交互
//             startButton.GetComponentInChildren<TextMeshProUGUI>().text = "battle now..."; // 修改按钮文本
//         }
//     }

//     // 初始化卡牌方法
//     private void InitializeCards()
//     {
//         for (int i = 0; i < cards.Length; i++)
//         {
//             cardUIs[i].Initialize(cards[i]); // 初始化每张卡牌的UI
//         }
//     }

//     // Awake 方法用于初始化单例模式
//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             //DontDestroyOnLoad(gameObject); // 跨场景保留
//         }
//         else
//         {
//             //Destroy(gameObject);
//         }

//         // 确保 Instance1 被正确设置
//         if (Instance1 == null)
//         {
//             Instance1 = this; // 或者设置为另一个实例
//         }
//     }

//     // 完全重置游戏时调用
//     public void ResetProgress()
//     {
//         _experience = 0; // 重置经验值
//     }


//     // 自动攻击协程
//     private IEnumerator AutoAttack()
//     {
//         Debug.Log("[调试] 自动攻击协程启动"); // 检查协程何时启动
//         while (isAutoMode) // 用标志控制循环
//         {
//             yield return new WaitForSeconds(0.5f); // 每 2 秒执行一次

//             // 检查Boss数组有效性
//             if (bosses == null || bosses.Length == 0)
//             {
//                 Debug.LogWarning("No bosses available"); // 警告没有可用的Boss
//                 yield break; // 退出协程
//             }

//             // 过滤已死亡的Boss
//             var aliveBosses = bosses.Where(b => b != null && !b.isDead).ToArray();
//             if (aliveBosses.Length == 0)
//             {
//                 Debug.Log("All bosses dead"); // 所有Boss已死亡
//                 yield break; // 退出协程
//             }

//             // 随机选择存活的Boss
//             Boss1 selectedBoss = aliveBosses[Random.Range(0, aliveBosses.Length)];

//             // 选择卡牌逻辑（示例随机选择）
//             CardUI selectedCard = cardUIs[Random.Range(0, cardUIs.Length)];

//             // 执行攻击
//             selectedBoss.TakeDamage1(selectedCard.card.damage);
//             Debug.Log($"Auto attack: {selectedCard.card.cardName} -> {selectedBoss.bossName}"); // 输出攻击日志
//         }
//     }
// }