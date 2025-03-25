using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    public string characterName;

    public List<CardDataSO> cardDatas;
    public List<Card> handCards;

    public void TakeDamage(int damage)
    {
        if(currentHP < damage)
        {
            currentHP = 0;
            Died();
        }
        currentHP -= damage;
    }

    public virtual void Died()
    {
        Debug.Log(this.characterName + "Died");
    }
}
