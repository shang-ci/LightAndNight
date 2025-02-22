// CharacterCard.cs
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterCard : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public string characterName;
    private RectTransform statsPanel;
    private TextMeshProUGUI[] statTexts;

    void Start()
    {
        statsPanel = UIManager.Instance.statsPanel;
        statTexts = UIManager.Instance.statTexts;
        Debug.Log($"CharacterCard 初始化：{characterName}");

        // 检查 characterName 是否为空或未赋值
        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogError("characterName 未赋值！");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"鼠标进入事件触发：{characterName}");

        if (CharacterDataBase.characters.TryGetValue(characterName, out CharacterData data))
        {
            UpdateStatsPanel(data);
            //PositionPanel(eventData.position);
            statsPanel.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError($"角色数据未找到：{characterName}");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标移出事件触发");

        statsPanel.gameObject.SetActive(false);
    }

    void UpdateStatsPanel(CharacterData data)
    {
        statTexts[0].text = data.character;
        statTexts[1].text = data.sincere.ToString();
        statTexts[2].text = data.brave.ToString();
        statTexts[3].text = data.fearless.ToString();
        statTexts[4].text = data.concentration.ToString();
        statTexts[5].text = data.faith.ToString();
        statTexts[6].text = data.happy.ToString();

        Debug.Log($"正在更新面板：{data.character}");

        if (statTexts == null || statTexts.Length < 7)
        {
            Debug.LogError($"statTexts未正确初始化！当前长度：{statTexts?.Length}");
            return;
        }

        // 逐个验证文本组件
        for (int i = 0; i < statTexts.Length; i++)
        {
            if (statTexts[i] == null)
            {
                Debug.LogError($"statTexts[{i}] 未分配！");
            }
        }

        // 强制设置测试值
        statTexts[1].text = "999"; // 测试是否能修改真挚值
    }

    // void PositionPanel(Vector2 mousePosition)
    // {
    //     RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //         statsPanel.parent.GetComponent<RectTransform>(),
    //         mousePosition,
    //         null,
    //         out Vector2 localPoint
    //     );
    //     statsPanel.anchoredPosition = localPoint + new Vector2(20, -20);
    // }
}