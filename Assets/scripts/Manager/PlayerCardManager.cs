using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

//为玩家管理卡牌的类
public class PlayerCardManager
{
    [Header("牌库")]
    public CardLibrarySO newGameLibrary;
    public CardLibrarySO currentLibrary;

    [Header("牌堆")]
    public List<CardDataSO> cardDatas;//所有卡牌数据——从execl拿到
    // 抽牌堆
    public List<CardDataSO> drawDeck = new List<CardDataSO>();
    // 弃牌堆
    public List<CardDataSO> discardDeck = new List<CardDataSO>();
    //手牌
    public List<Card> handCards;

    [Header("卡牌布局")]
    public Vector3 deckPosition;//抽出来的卡牌的位置的初始位置
    public CardLayoutManager layoutManager;//卡牌布局管理器

    public CharacterBase owner; // 卡牌拥有者

    //初始化
    public PlayerCardManager(CardLibrarySO _newGameLibrary, CharacterBase _owner,CardLayoutManager _cardLayoutManager)
    {
        cardDatas = new List<CardDataSO>();
        handCards = new List<Card>();
        newGameLibrary = _newGameLibrary;
        owner = _owner;
        layoutManager = _cardLayoutManager;

        // 确保 currentLibrary 已初始化
        currentLibrary = ScriptableObject.CreateInstance<CardLibrarySO>();
        currentLibrary.entryList = new List<CardLibraryEntry>();

        foreach (var item in newGameLibrary.entryList)
        {
            currentLibrary.entryList.Add(item);
        }
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

        // 洗牌/更新抽牌堆or弃牌堆的数字
        ShuffleDeck();
    }



    //抽取卡牌，从抽牌堆中抽取卡牌，放入手牌中——每回合开始时调用
    public void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0)
            {
                // 抽牌堆为空，从弃牌堆中重新生成
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);
                }
                ShuffleDeck();
            }

            // 从抽牌堆中抽一张牌——抽的是卡牌数据，赋值给卡牌预制对象
            CardDataSO cardData = drawDeck[0];
            drawDeck.RemoveAt(0);

            // 更新 UI 数字
            //drawCountEvent.RaiseEvent(drawDeck.Count, this);

            //拿到卡牌预制对象
            var card = GetCardObject().GetComponent<Card>();

            // 初始化——把卡牌数据赋值给卡牌预制对象
            card.InitCard(cardData, owner);
            card.transform.position = deckPosition;

            // 将这张牌添加到手牌中
            handCards.Add(card);
            var delay = i * 0.2f;

            // 设置卡牌的位置、旋转角度
            SetCardLayout(delay);
        }
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    private void ShuffleDeck()
    {
        //在这个游戏里就是每次洗牌都要确保弃牌堆是空的
        discardDeck.Clear();

        //// 更新 UI 显示数量——抽牌堆和弃牌堆的数量的变化也属于UI更新部分，通过事件广播来也许可以统一交给UI更新的部分管理
        //drawCountEvent.RaiseEvent(drawDeck.Count, this);
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);

        //这里的洗牌算法是每次随机交换两张牌的位置，但是在别的游戏里可能会有考虑概率的洗牌算法
        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSO temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
    }

    /// <summary>
    /// 弃牌逻辑，在打出牌的事件中调用，会接受一个卡牌对象，将其放入弃牌堆中
    /// </summary>
    /// <param name="obj"></param>
    public void DiscardCard(object obj)
    {
        Card card = obj as Card;

        //discardDeck.Add(card.GetOriginalCardData());//把初始数据放入弃牌堆——保持每次抽牌都是用的初始数据
        discardDeck.Add(card.cardDataSO);
        handCards.Remove(card);

        PoolToolDiscardCard(card.gameObject);//卡牌池回收

        // 更新弃牌堆 UI
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);
        Debug.Log("弃牌堆数量：" + discardDeck.Count);

        SetCardLayout(0);
    }

    //随机x张弃牌
    public void DiscardRandomCard(int value)
    {
        for (int i = 0; i < value; i++)
        {
            if (handCards.Count > 0)
            {
                int randomIndex = Random.Range(0, handCards.Count);
                Card cardToDiscard = handCards[randomIndex];

                DiscardCard(cardToDiscard);
            }
        }
    }

    /// <summary>
    /// 事件监听函数——玩家回合结束
    /// </summary>
    public void DisAllHandCardsOnPlayerTurnEnd()
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            //discardDeck.Add(handCards[i].cardDataSO);
            //PoolToolDiscardCard(handCards[i].gameObject);
            DiscardCard(handCards[i]);
        }

        handCards.Clear();

        Debug.Log("玩家回合结束，弃牌堆数量：" + discardDeck.Count);
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);
    }

    //回收所有卡牌——当游戏结束时调用——玩家或者怪物死亡时
    public void ReleaseAllCards()
    {
        foreach (var card in handCards)
        {
            PoolToolDiscardCard(card.gameObject);
        }

        handCards.Clear();
        //InitializeDeck();//在结束时重新初始化卡牌堆，防止下一轮粗线错误——必须要在解锁新卡牌之后再初始化卡牌堆
    }

    //回收卡牌
    public void PoolToolDiscardCard(GameObject cardObj)
    {
        PoolTool.instance.ReleaseObjectToPool(cardObj);
    }

    private void SetCardLayout(float delay)
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            Card currentCard = handCards[i];

            CardTransform cardTransform = layoutManager.GetCardTransform(i, handCards.Count);

            // currentCard.transform.SetPositionAndRotation(cardTransform.pos, cardTransform.rotation);——这个是直接设置位置和旋转角度，没有动画效果

            // 卡牌能量判断加灰色效果
            currentCard.UpdateCardState();

            // 卡牌动画
            currentCard.isAnimating = true;
            //让卡牌从缩放为0的状态变为正常大小，而且是每个新抽出来的卡才放大，因为之前的已经是正常大小了所以只有薪酬出来的卡才放大
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () =>
            {
                //卡牌放大完毕后，将卡牌移动到指定位置，有一个连续发拍的效果
                currentCard.transform.DOMove(cardTransform.pos, 0.5f).onComplete = () =>
                {
                    currentCard.isAnimating = false;
                };
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };

            // 设置卡牌排序层级，保证卡牌的显示顺序
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            //保存下卡牌的位置和旋转角度，方便选中后有个上移的提牌效果可以返回——但具体是用卡牌的animator部分实现的，还原的话也只用归零就好
            currentCard.UpdatePositionRotation(cardTransform.pos, cardTransform.rotation);
        }
    }


    /// <summary>
    /// 抽卡时调用的函数获得卡牌 GameObject——这里的是从对象池中获取卡牌，对象池里有7个卡牌，也就是说最多同时存在7个卡牌在手牌中
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardObject()
    {
        var cardObj = PoolTool.instance.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;//将卡牌缩放为0,有一个放大动画
        return cardObj;
    }

}
