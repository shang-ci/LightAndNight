using EventSystem;
using UnityEngine;

public class Player : CharacterBase
{
    public CardLayoutManager layoutManager;//���Ʋ��ֹ�����
    public PlayerCardManager cardManager;

    public CardLibrarySO library;//��ų�ʼ���ƿ�

    public override void SetCharacterBase(string _name,int _id)
    {
        base.SetCharacterBase(_name, _id);
        this.maxMP = 100;
        this.currentMP = 100;
        this.maxHP = 100;
        this.currentHP = 100;
        this.characterName = _name;
        this.characterID = _id;
    }

    public override void Awake()
    {
        base.Awake();
        cardManager = new PlayerCardManager(library, this, layoutManager);
        SetCharacterBase("player1", 101);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener("PlayerTurnBegin", newTurn);
        EventManager.Instance.AddListener("PlayerTurnBegin", NewTurnDrawCards);
        EventManager.Instance.AddListener("PlayerTurnEnd", cardManager.DisAllHandCardsOnPlayerTurnEnd);//��һغϽ�����������������
        EventManager.Instance.AddListener<object>("DiscardCard", cardManager.DiscardCard);//���ƴ�����յ����ƶ�
        EventManager.Instance.AddListener<int>("DiscardRandomCard", cardManager.DiscardRandomCard);//������ơ�����������
        EventManager.Instance.AddListener<int>("DrawCard", cardManager.DrawCard);//���ơ�����������
        EventManager.Instance.AddListener("GameOver", cardManager.ReleaseAllCards);//��Ϸ�������������п���
        EventManager.Instance.AddListener("GameWin", cardManager.ReleaseAllCards);//��Ϸʤ�����������п���
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener("PlayerTurnBegin", newTurn);
        EventManager.Instance.RemoveListener("PlayerTurnBegin", NewTurnDrawCards);
        EventManager.Instance.RemoveListener("PlayerTurnEnd", cardManager.DisAllHandCardsOnPlayerTurnEnd);
        EventManager.Instance.RemoveListener<object>("DiscardCard", cardManager.DiscardCard);
        EventManager.Instance.RemoveListener<int>("DiscardRandomCard", cardManager.DiscardRandomCard);
        EventManager.Instance.RemoveListener<int>("DrawCard", cardManager.DrawCard);
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
}
