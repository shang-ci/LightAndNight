using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private List<Card> initialCards;
    [SerializeField] private Transform handContainer;

    private List<Card> _currentCards = new();

    public void ResetHand()
    {
        // 销毁当前手牌
        foreach (var card in _currentCards)
        {
            Destroy(card.gameObject);
        }
        _currentCards.Clear();

        // 重新生成初始手牌
        foreach (var cardPrefab in initialCards)
        {
            var newCard = Instantiate(cardPrefab, handContainer);
            _currentCards.Add(newCard);
        }
    }
}