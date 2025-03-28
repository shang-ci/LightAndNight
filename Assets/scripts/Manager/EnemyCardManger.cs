using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class EnemyCardManger
{
    public CardLibrarySO currentLibrary;
    public List<CardDataSO> drawDeck = new List<CardDataSO>();
    public List<Card> handCards;
    public CharacterBase owner; // ����ӵ����
    public Transform cardParent;//������Ŀ��Ƶ�λ�õĳ�ʼλ��

    public EnemyCardManger(CardLibrarySO _currentLibrary, CharacterBase _owner,Transform _parent)
    {
        handCards = new List<Card>();
        owner = _owner;
        currentLibrary = _currentLibrary;
        cardParent = _parent;
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
    }

    //��ȡ���ƣ��ӳ��ƶ��г�ȡ���ƣ����������С���ÿ�غϿ�ʼʱ����
    public void DrawCard(int amount)
    {
        if (drawDeck.Count == 0)
        {
            Debug.LogError("Draw deck is empty!");
            return;
        }

        // �ӳ��ƶ��������ȡһ����
        int randomIndex = Random.Range(0, drawDeck.Count);
        CardDataSO cardData = drawDeck[randomIndex];
        //drawDeck.RemoveAt(randomIndex);

        // �õ�����Ԥ�ƶ���
        var card = GetCardObject().GetComponent<Card>();

        // ��ʼ�������ѿ������ݸ�ֵ������Ԥ�ƶ���
        card.InitCard(cardData, owner);

        card.transform.SetParent(cardParent);
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;

        // ����������ӵ�������
        handCards.Add(card);

        // ��ӡ������Ϣ
        Debug.Log($"Drew card: {cardData.name}");
    }

    public GameObject GetCardObject()
    {
        var cardObj = PoolTool.instance.GetObjectFromPool();
        cardObj.transform.localScale = Vector3.zero;//����������Ϊ0,��һ���Ŵ󶯻�
        return cardObj;
    }
}
