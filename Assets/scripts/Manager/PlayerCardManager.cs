using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

//Ϊ��ҹ����Ƶ���
public class PlayerCardManager
{
    [Header("�ƿ�")]
    public CardLibrarySO newGameLibrary;
    public CardLibrarySO currentLibrary;

    [Header("�ƶ�")]
    public List<CardDataSO> cardDatas;//���п������ݡ�����execl�õ�
    // ���ƶ�
    public List<CardDataSO> drawDeck = new List<CardDataSO>();
    // ���ƶ�
    public List<CardDataSO> discardDeck = new List<CardDataSO>();
    //����
    public List<Card> handCards;

    [Header("���Ʋ���")]
    public Vector3 deckPosition;//������Ŀ��Ƶ�λ�õĳ�ʼλ��
    public CardLayoutManager layoutManager;//���Ʋ��ֹ�����

    public CharacterBase owner; // ����ӵ����

    //��ʼ��
    public PlayerCardManager(CardLibrarySO _newGameLibrary, CharacterBase _owner,CardLayoutManager _cardLayoutManager)
    {
        cardDatas = new List<CardDataSO>();
        handCards = new List<Card>();
        newGameLibrary = _newGameLibrary;
        owner = _owner;
        layoutManager = _cardLayoutManager;

        // ȷ�� currentLibrary �ѳ�ʼ��
        currentLibrary = ScriptableObject.CreateInstance<CardLibrarySO>();
        currentLibrary.entryList = new List<CardLibraryEntry>();

        foreach (var item in newGameLibrary.entryList)
        {
            currentLibrary.entryList.Add(item);
        }
    }


    //��ʼ���������ƿ��еĿ��Ƹ��Ƶ����ƶ��С���ÿ����һ��ս��ʱ���á������ƶ��õ��Ķ���ԭʼ��������
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

        // ϴ��/���³��ƶ�or���ƶѵ�����
        ShuffleDeck();
    }



    //��ȡ���ƣ��ӳ��ƶ��г�ȡ���ƣ����������С���ÿ�غϿ�ʼʱ����
    public void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0)
            {
                // ���ƶ�Ϊ�գ������ƶ�����������
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);
                }
                ShuffleDeck();
            }

            // �ӳ��ƶ��г�һ���ơ�������ǿ������ݣ���ֵ������Ԥ�ƶ���
            CardDataSO cardData = drawDeck[0];
            drawDeck.RemoveAt(0);

            // ���� UI ����
            //drawCountEvent.RaiseEvent(drawDeck.Count, this);

            //�õ�����Ԥ�ƶ���
            var card = GetCardObject().GetComponent<Card>();

            // ��ʼ�������ѿ������ݸ�ֵ������Ԥ�ƶ���
            card.InitCard(cardData, owner);
            card.transform.position = deckPosition;

            // ����������ӵ�������
            handCards.Add(card);
            var delay = i * 0.2f;

            // ���ÿ��Ƶ�λ�á���ת�Ƕ�
            SetCardLayout(delay);
        }
    }

    /// <summary>
    /// ϴ��
    /// </summary>
    private void ShuffleDeck()
    {
        //�������Ϸ�����ÿ��ϴ�ƶ�Ҫȷ�����ƶ��ǿյ�
        discardDeck.Clear();

        //// ���� UI ��ʾ�����������ƶѺ����ƶѵ������ı仯Ҳ����UI���²��֣�ͨ���¼��㲥��Ҳ�����ͳһ����UI���µĲ��ֹ���
        //drawCountEvent.RaiseEvent(drawDeck.Count, this);
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);

        //�����ϴ���㷨��ÿ��������������Ƶ�λ�ã������ڱ����Ϸ����ܻ��п��Ǹ��ʵ�ϴ���㷨
        for (int i = 0; i < drawDeck.Count; i++)
        {
            CardDataSO temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
    }

    /// <summary>
    /// �����߼����ڴ���Ƶ��¼��е��ã������һ�����ƶ��󣬽���������ƶ���
    /// </summary>
    /// <param name="obj"></param>
    public void DiscardCard(object obj)
    {
        Card card = obj as Card;

        //discardDeck.Add(card.GetOriginalCardData());//�ѳ�ʼ���ݷ������ƶѡ�������ÿ�γ��ƶ����õĳ�ʼ����
        discardDeck.Add(card.cardDataSO);
        handCards.Remove(card);

        PoolToolDiscardCard(card.gameObject);//���Ƴػ���

        // �������ƶ� UI
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);
        Debug.Log("���ƶ�������" + discardDeck.Count);

        SetCardLayout(0);
    }

    //���x������
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
    /// �¼���������������һغϽ���
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

        Debug.Log("��һغϽ��������ƶ�������" + discardDeck.Count);
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);
    }

    //�������п��ơ�������Ϸ����ʱ���á�����һ��߹�������ʱ
    public void ReleaseAllCards()
    {
        foreach (var card in handCards)
        {
            PoolToolDiscardCard(card.gameObject);
        }

        handCards.Clear();
        //InitializeDeck();//�ڽ���ʱ���³�ʼ�����ƶѣ���ֹ��һ�ִ��ߴ��󡪡�����Ҫ�ڽ����¿���֮���ٳ�ʼ�����ƶ�
    }

    //���տ���
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

            // currentCard.transform.SetPositionAndRotation(cardTransform.pos, cardTransform.rotation);���������ֱ������λ�ú���ת�Ƕȣ�û�ж���Ч��

            // ���������жϼӻ�ɫЧ��
            currentCard.UpdateCardState();

            // ���ƶ���
            currentCard.isAnimating = true;
            //�ÿ��ƴ�����Ϊ0��״̬��Ϊ������С��������ÿ���³�����Ŀ��ŷŴ���Ϊ֮ǰ���Ѿ���������С������ֻ��н������Ŀ��ŷŴ�
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () =>
            {
                //���ƷŴ���Ϻ󣬽������ƶ���ָ��λ�ã���һ���������ĵ�Ч��
                currentCard.transform.DOMove(cardTransform.pos, 0.5f).onComplete = () =>
                {
                    currentCard.isAnimating = false;
                };
                currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            };

            // ���ÿ�������㼶����֤���Ƶ���ʾ˳��
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            //�����¿��Ƶ�λ�ú���ת�Ƕȣ�����ѡ�к��и����Ƶ�����Ч�����Է��ء������������ÿ��Ƶ�animator����ʵ�ֵģ���ԭ�Ļ�Ҳֻ�ù���ͺ�
            currentCard.UpdatePositionRotation(cardTransform.pos, cardTransform.rotation);
        }
    }


    /// <summary>
    /// �鿨ʱ���õĺ�����ÿ��� GameObject����������ǴӶ�����л�ȡ���ƣ����������7�����ƣ�Ҳ����˵���ͬʱ����7��������������
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardObject()
    {
        var cardObj = PoolTool.instance.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;//����������Ϊ0,��һ���Ŵ󶯻�
        return cardObj;
    }

}
