using UnityEngine;
using System.Collections.Generic;

public class AttributePanel : MonoBehaviour
{
    public Dictionary<string, UnityEngine.UI.Text[]> attributeTexts; // 键：卡牌ID，值：各属性文本数组

    public static object Instance { get; internal set; }



    public void UpdateCard(Card card)
    {
        var texts = attributeTexts[card.id];
        for (int i = 0; i < 6; i++)
        {
            texts[i].text = $"属性{i+1}: {card.attributes[i]:.1.2f}";

            Debug.Log("1f属性 -> 1.2f属性");
        }
    }
}