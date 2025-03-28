using UnityEngine;

public class Player : CharacterBase
{
    public CardLayoutManager layoutManager;//���Ʋ��ֹ�����
    public PlayerCardManager cardManager;

    public CardLibrarySO library;//��ų�ʼ���ƿ�

    public override void SetCharacterBase(string _name)
    {
        base.SetCharacterBase(_name);
        this.maxMP = 100;
        this.currentMP = 100;
        this.maxHP = 100;
        this.currentHP = 100;
        this.characterName = _name;
    }

    public override void Awake()
    {
        base.Awake();
        cardManager = new PlayerCardManager(library, this, layoutManager);
        SetCharacterBase("player1");
    }

    private void Start()
    {
        cardManager.InitializeDeck();//��ʼ�����ƶ�
        //StartFlashing();
    }

    [ContextMenu("���Գ���")]
    public void TestDrawCard()
    {
        cardManager.DrawCard(1);
    }
}
