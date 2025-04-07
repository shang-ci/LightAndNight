using EventSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : CharacterBase,IPointerClickHandler
{
    public CardLayoutManager layoutManager;//���Ʋ��ֹ�����
    public PlayerCardManager cardManager;
    public EquipManager equipManager;//װ��������
    public CardLibrarySO library;//��ų�ʼ���ƿ�

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
        EventManager.Instance.AddListener("PlayerTurnEnd", cardManager.DisAllHandCardsOnPlayerTurnEnd);//��һغϽ�����������������
        EventManager.Instance.AddListener<object>("PlayerUseCard", cardManager.DiscardCard);//���ƴ�����յ����ƶ�
        EventManager.Instance.AddListener<int>("DiscardRandomCard", cardManager.DiscardRandomCard);//������ơ�����������
        EventManager.Instance.AddListener<int>("PlayerDrawCard", cardManager.DrawCard);//���ơ�����������
        EventManager.Instance.AddListener("GameOver", cardManager.ReleaseAllCards);//��Ϸ�������������п���
        EventManager.Instance.AddListener("GameWin", cardManager.ReleaseAllCards);//��Ϸʤ�����������п���
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
        cardManager.InitializeDeck();//��ʼ�����ƶ�
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }


    [ContextMenu("���Գ���")]
    public void TestDrawCard()
    {
        cardManager.DrawCard(1);
    }

    /// <summary>
    /// �¼���������������һغϿ�ʼʱ���á�����ȡ4�ſ���
    /// </summary>
    public void NewTurnDrawCards()
    {
        cardManager.DrawCard(4);
    }


    //��ʼ��������ݡ�������menu����������Ϸ��ťʱ����
    public void NewGame()
    {
        currentHP = maxHP;
        isDead = false;
        newTurn();//���÷���ֵ
    }

    public void newTurn()
    {
        currentMP = maxMP;
    }

    //�����¼�����UI������ȥ��������UI
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
