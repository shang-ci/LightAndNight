using UnityEngine;

public class EnhancementSystem : MonoBehaviour
{
    public void EnhanceAttribute(Card card, int attributeIndex, int cost)
    {
        // 检查条件后执行强化
        card.attributes[attributeIndex] *= 1.1f;
        //AttributePanel.Instance.UpdateCard(card);
        ((AttributePanel)AttributePanel.Instance).UpdateCard(card);
    }
}
