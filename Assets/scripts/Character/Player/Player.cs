using UnityEngine;

public class Player : CharacterBase
{
    public CardLayoutManager layoutManager;//ø®≈∆≤ºæ÷π‹¿Ì∆˜
    [SerializeField]public PlayerCardManager cardManager;

    public CardLibrarySO library;//¥Ê∑≈≥ı ºø®≈∆ø‚

    private void Awake()
    {
        cardManager = new PlayerCardManager(library, this, layoutManager);
        SetPlayer("player1",100);
    }

    private void Start()
    {
        cardManager.InitializeDeck();//≥ı ºªØ≥È≈∆∂—
    }

    [ContextMenu("≤‚ ‘≥È≈∆")]
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
