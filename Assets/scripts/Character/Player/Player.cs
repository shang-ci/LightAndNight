using UnityEngine;

public class Player : CharacterBase
{
    public CardLayoutManager layoutManager;//���Ʋ��ֹ�����
    [SerializeField]public PlayerCardManager cardManager;

    public CardLibrarySO library;//��ų�ʼ���ƿ�

    private void Awake()
    {
        cardManager = new PlayerCardManager(library, this, layoutManager);
        SetPlayer("player1",100);
    }

    private void Start()
    {
        cardManager.InitializeDeck();//��ʼ�����ƶ�
    }

    [ContextMenu("���Գ���")]
    public void TestDrawCard()
    {
        cardManager.DrawCard(1);
    }


    public void SetPlayer(string playerName, int maxHP)
    {
        this.characterName = playerName;
        this.maxHP = maxHP;
    }
}
