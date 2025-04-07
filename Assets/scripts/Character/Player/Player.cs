using EventSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : CharacterBase,IPointerClickHandler
{
    public CardLayoutManager layoutManager;//卡牌布局管理器
    public PlayerCardManager cardManager;
    public EquipManager equipManager;//装备管理器
    public CardLibrarySO library;//存放初始卡牌库

    public override void SetCharacterBase(PlayerData _playerData)
    {
        this.maxMP = 100;
        this.currentMP = 100;
        this.maxHP = 100;
        this.currentHP = 100;
        this.characterName = _playerData.playerName;
        this.characterID = _playerData.playerId;
        this.library = _playerData.library;
    }

    public override void Awake()
    {
        base.Awake();
        cardManager = new PlayerCardManager(library, this, layoutManager);
        //SetCharacterBase("player1", 101);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener("PlayerTurnBegin", newTurn);
        EventManager.Instance.AddListener("PlayerTurnBegin", NewTurnDrawCards);
        EventManager.Instance.AddListener("PlayerTurnEnd", cardManager.DisAllHandCardsOnPlayerTurnEnd);//玩家回合结束，弃掉所有手牌
        EventManager.Instance.AddListener<object>("PlayerUseCard", cardManager.DiscardCard);//卡牌打出回收到弃牌堆
        EventManager.Instance.AddListener<int>("DiscardRandomCard", cardManager.DiscardRandomCard);//随机弃牌——卡牌能力
        EventManager.Instance.AddListener<int>("PlayerDrawCard", cardManager.DrawCard);//抽牌——卡牌能力
        EventManager.Instance.AddListener("GameOver", cardManager.ReleaseAllCards);//游戏结束，弃掉所有卡牌
        EventManager.Instance.AddListener("GameWin", cardManager.ReleaseAllCards);//游戏胜利，弃掉所有卡牌
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener("PlayerTurnBegin", newTurn);
        EventManager.Instance.RemoveListener("PlayerTurnBegin", NewTurnDrawCards);
        EventManager.Instance.RemoveListener("PlayerTurnEnd", cardManager.DisAllHandCardsOnPlayerTurnEnd);
        EventManager.Instance.RemoveListener<object>("PlayerUseCard", cardManager.DiscardCard);
        EventManager.Instance.RemoveListener<int>("DiscardRandomCard", cardManager.DiscardRandomCard);
        EventManager.Instance.RemoveListener<int>("PlayerDrawCard", cardManager.DrawCard);
        EventManager.Instance.RemoveListener("GameOver", cardManager.ReleaseAllCards);
        EventManager.Instance.RemoveListener("GameWin", cardManager.ReleaseAllCards);
    }

    private void Start()
    {
        cardManager.InitializeDeck();//初始化抽牌堆
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }


    [ContextMenu("测试抽牌")]
    public void TestDrawCard()
    {
        cardManager.DrawCard(1);
    }

    /// <summary>
    /// 事件监听函数——玩家回合开始时调用——抽取4张卡牌
    /// </summary>
    public void NewTurnDrawCards()
    {
        cardManager.DrawCard(4);
    }


    //初始化玩家数据——当在menu界面点击新游戏按钮时调用
    public void NewGame()
    {
        currentHP = maxHP;
        isDead = false;
        newTurn();//重置法力值
    }

    public void newTurn()
    {
        currentMP = maxMP;
    }

    //触发事件，让UI管理器去监听更新UI
    public void UpdateMana(int cost)
    {
        currentMP -= cost;
        if (currentMP <= 0)
        {
            currentMP = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerManager.instance.ChangeCurrentPlayer(this);
    }
}
