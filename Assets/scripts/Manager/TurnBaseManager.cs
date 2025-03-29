using EventSystem;
using System.Collections.Generic;
using UnityEngine;

//战斗中的回合管理、是否开启战斗、玩家的显示控制
public class TurnBaseManager : MonoBehaviour
{
    public static TurnBaseManager instance;

    [SerializeField]private bool isPlayerTurn = false;
    [SerializeField]private bool isEnemyTurn = false;
    public bool battleEnd = true;//战斗是否结束——只有处于战斗时，才会进行回合管理

    [Header("阶段管理")]
    private float timeCounter;//计时器，管理每个回合的时间
    public float enemyTurnDuration;
    public float playerTurnDuration;//玩家没有时间要求，只有敌人有时间要求——或者给玩家限制时间
    public float drawCardDuration;//抽牌时间——抽牌阶段

    [Header("回合计数器")]
    private int playerTurnCount = 0; // 玩家回合计数——也许玩家需要记录回合数可以在别的地方进行相关设计
    private int enemyTurnCount = 0; // 敌人回合计数

    [Header("事件广播")]
    public ObjectEventSO playerTurnBegin;//玩家回合结束用敌人回合开始代替表示，也可以在这里把抽牌阶段抽取出来
    public ObjectEventSO enemyTurnBegin;
    public ObjectEventSO enemyTurnEnd;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener("PlayerTurnEnd", EnemyTurnBegin);//玩家回合结束，敌人回合开始
        EventManager.Instance.AddListener<object>("GameOver", StopTurnBaseSystem);//战斗结束，停止回合管理
        EventManager.Instance.AddListener("NewGame", NewGame);//开始菜单点击新游戏，初始化玩家
        EventManager.Instance.AddListener("GameStart", GameStart);//进入房间/遇见战斗事件，开启回合管理
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener("PlayerTurnEnd", EnemyTurnBegin);
        EventManager.Instance.RemoveListener<object>("GameOver", StopTurnBaseSystem);
        EventManager.Instance.RemoveListener("NewGame", NewGame);
        EventManager.Instance.RemoveListener("GameStart", GameStart);
    }

    private void Update()
    {
        if (battleEnd)
        {
            return;
        }

        if (isEnemyTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= enemyTurnDuration)
            {
                timeCounter = 0;
                // 敌人回合结束
                EnemyTurnEnd();
            }
        }

        if (isPlayerTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= playerTurnDuration)
            {
                timeCounter = 0f;
                // 玩家回合开始
                PlayerTurning();
                isPlayerTurn = false;
            }
        }
    }

    //开启战斗时就开启回合管理——一般是进入战斗调用
    [ContextMenu("Game Start")]
    public void GameStart()
    {
        //清除玩家身上的所有状态
        //player = playerObj.GetComponent<Player>();
        //TOOD：这里返回的敌人可以是多个，所以不能只用Enemy类型接收，以后要改成Enemy[]

        isPlayerTurn = true;
        isEnemyTurn = false;
        battleEnd = false;
        timeCounter = 0;
        playerTurnCount = 0;
        enemyTurnCount = 0;
    }

    public void PlayerTurning()
    {
        playerTurnCount++;
        playerTurnBegin.RaiseEvent(null, this);
        EventManager.Instance.TriggerEvent("PlayerTurnBegin");
        //player.UpdateStatusEffectRounds();//更新玩家状态效果回合数
    }

    //玩家回合结束调用——当点击回合转换按钮时玩家回合结束
    public void EnemyTurnBegin()
    {
        enemyTurnCount++;
        isEnemyTurn = true;
        EventManager.Instance.TriggerEvent("EnemyTurnBegin");//敌人回合触发——敌人攻击
        //enemy.UpdateStatusEffectRounds();//更新敌人状态效果回合数
    }

    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEnd.RaiseEvent(null, this);
        isPlayerTurn = true;
    }





    //停止对战——当对战去的胜负时调用——gameover、loadma事件——停止回合管理
    public void StopTurnBaseSystem(object obj)
    {
        battleEnd = true;
        //playerObj.SetActive(false);
    }

    //初始化玩家——在点击新游戏时调用，因为玩家自身是隐藏的无法监听到新游戏事件——开启新游戏时
    public void NewGame()
    {
        foreach(var item in GameManager.instance.playerCharacters)
        {
            item.GetComponent<Player>().NewGame();
        }
    }
}
