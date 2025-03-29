using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class EnemyCardManger
{
    public CardLibrarySO currentLibrary;
    public List<CardDataSO> drawDeck = new List<CardDataSO>();
    public List<Card> handCards;
    public CharacterBase owner; // 卡牌拥有者
    public Transform cardParent;//抽出来的卡牌的位置的初始位置

    public EnemyCardManger(CardLibrarySO _currentLibrary, CharacterBase _owner,Transform _parent)
    {
        handCards = new List<Card>();
        owner = _owner;
        currentLibrary = _currentLibrary;
        cardParent = _parent;
    }

    //初始化，将卡牌库中的卡牌复制到抽牌堆中——每开启一轮战斗时调用——抽牌堆拿到的都是原始卡牌数据
    public void InitializeDeck()
    {
        drawDeck.Clear();
        foreach (var entry in currentLibrary.entryList)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }
    }

    /// <summary>
    /// 事件监听函数——敌人回合开始时调用——抽取1张卡牌
    /// </summary>
    public void NewTurnDrawCards()
    {
        DrawCard(1);
    }


    //抽取卡牌，从抽牌堆中抽取卡牌，放入手牌中——每回合开始时调用
    public void DrawCard(int amount)
    {
        if (drawDeck.Count == 0)
        {
            Debug.LogError("Draw deck is empty!");
            return;
        }

        // 从抽牌堆中随机抽取一张牌
        int randomIndex = Random.Range(0, drawDeck.Count);
        CardDataSO cardData = drawDeck[randomIndex];
        //drawDeck.RemoveAt(randomIndex);

        // 拿到卡牌预制对象
        var card = GetCardObject().GetComponent<Card>();

        // 初始化——把卡牌数据赋值给卡牌预制对象
        card.InitCard(cardData, owner);

        card.transform.SetParent(cardParent);
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
        card.transform.localScale = Vector3.one;

        // 将这张牌添加到手牌中
        handCards.Add(card);

        // 打印调试信息
        Debug.Log($"Drew card: {cardData.name}");
    }

    /// <summary>
    /// 弃牌逻辑，在打出牌的事件中调用，会接受一个卡牌对象，将其放入弃牌堆中
    /// </summary>
    /// <param name="obj"></param>
    public void DiscardCard(object obj)
    {
        Card card = obj as Card;
        PoolToolDiscardCard(card.gameObject);//卡牌池回收

        // 更新弃牌堆 UI
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);
        Debug.Log("敌人弃牌");

    }

    //回收卡牌
    public void PoolToolDiscardCard(GameObject cardObj)
    {
        PoolTool.instance.ReleaseObjectToPool(cardObj);
    }

    public GameObject GetCardObject()
    {
        var cardObj = PoolTool.instance.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;//将卡牌缩放为0,有一个放大动画
        return cardObj;
    }
}
