using UnityEngine;

public class Player : CharacterBase
{
    public CardLayoutManager layoutManager;//卡牌布局管理器

    private void Awake()
    {
        SetPlayer("player1",100);
    }

    public void SetPlayer(string playerName, int maxHP)
    {
        this.characterName = playerName;
        this.maxHP = maxHP;
    }
}
