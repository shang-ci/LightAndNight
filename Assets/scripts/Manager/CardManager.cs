using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管理卡牌的类
public class CardManager
{
    public List<CardDataSO> cardDatas;
    public List<Card> handCards;

    public CardManager()
    {
        cardDatas = new List<CardDataSO>();
        handCards = new List<Card>();
    }
}
