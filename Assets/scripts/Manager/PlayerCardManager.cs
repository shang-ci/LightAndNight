using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using DG.Tweening;

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


    public CardLayoutManager layoutManager;//���Ʋ��ֹ�����

    //��ʼ��
    public PlayerCardManager(CardLibrarySO _newGameLibrary)
    {
        cardDatas = new List<CardDataSO>();
        handCards = new List<Card>();
        newGameLibrary = _newGameLibrary;

        foreach (var item in newGameLibrary.entryList)
        {
            currentLibrary.entryList.Add(item);
        }

        //foreach (var entry in currentLibrary.entryList)
        //{
        //    for (int i = 0; i < entry.amount; i++)
        //    {
        //        drawDeck.Add(entry.cardData);
        //    }
        //}
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

        //DiscardCard(card.gameObject);//���Ƴػ���

        // �������ƶ� UI
        //discardCountEvent.RaiseEvent(discardDeck.Count, this);

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
            //currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () => {
            //    //���ƷŴ���Ϻ󣬽������ƶ���ָ��λ�ã���һ���������ĵ�Ч��
            //    currentCard.transform.DOMove(cardTransform.pos, 0.5f).onComplete = () => {
            //        currentCard.isAnimating = false;
            //    };
            //    currentCard.transform.DORotateQuaternion(cardTransform.rotation, 0.5f);
            //};

            // ���ÿ�������㼶����֤���Ƶ���ʾ˳��
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            //�����¿��Ƶ�λ�ú���ת�Ƕȣ�����ѡ�к��и����Ƶ�����Ч�����Է��ء������������ÿ��Ƶ�animator����ʵ�ֵģ���ԭ�Ļ�Ҳֻ�ù���ͺ�
            currentCard.UpdatePositionRotation(cardTransform.pos, cardTransform.rotation);
        }
    }



}
