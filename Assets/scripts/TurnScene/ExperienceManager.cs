using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    private TextMeshProUGUI experienceText1;

    private TextMeshProUGUI experienceText2;

    private void Start()
    {
        experienceText1 = GetComponent<TextMeshProUGUI>();
        UpdateExperienceDisplay();
    }

    // 更新显示（在数据变化时调用）
    public void UpdateExperienceDisplay()
    {
        experienceText1.text = $"经验值: {MenuManager.Instance.playerExperience[0]}";

        experienceText2.text = $"经验值: {MenuManager.Instance.playerExperience[1]}";
    }

    // 示例：增加经验值的方法（可绑定到其他事件）
    // public void AddExperience(int amount)
    // {
    //     DataManager.Instance.Experience += amount;
    //     UpdateExperienceDisplay();
    // }
}