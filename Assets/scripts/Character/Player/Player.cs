using UnityEngine;

public class Player : CharacterBase
{

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
