using TMPro;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expText1;

    [SerializeField] private TextMeshProUGUI expText2;

    [SerializeField] private TextMeshProUGUI nameText1;

    [SerializeField] private TextMeshProUGUI nameText2;

    [SerializeField] private TextMeshProUGUI levelText1;

    [SerializeField] private TextMeshProUGUI levelText2;

    //[SerializeField] private float updateSpeed = 0.5f;

    //private float updateTimer;

    private void Start()
    {
        // expText1 = GetComponent<TextMeshProUGUI>();
        // expText2 = GetComponent<TextMeshProUGUI>();
        // nameText1 = GetComponent<TextMeshProUGUI>();
        // nameText2 = GetComponent<TextMeshProUGUI>();
        // levelText1 = GetComponent<TextMeshProUGUI>();
        // levelText2 = GetComponent<TextMeshProUGUI>();
        // UpdateExperienceDisplay();
    }

    //  void Start()
    // {
    //     // 确保所有引用已正确分配
    //     Debug.Assert(expText1 != null, "expText1 未分配");
    //     Debug.Assert(expText2 != null, "expText2 未分配");
    //     Debug.Assert(levelText1 != null, "levelText1 未分配");
    //     Debug.Assert(levelText2 != null, "levelText2 未分配");
    //     Debug.Assert(nameText1 != null, "nameText1 未分配");
    //     Debug.Assert(nameText2 != null, "nameText2 未分配");
    //     Debug.Assert(MenuManager.Instance != null, "MenuManager.Instance 未初始化");
    //     Debug.Assert(LevelManager.Instance != null, "LevelManager.Instance 未初始化");
    // }

    // 更新显示（在数据变化时调用）
    public void UpdateExperienceDisplay()
    {
        expText1.text = $"经验值: {MenuManager.Instance.playerExperience[0]}";

        expText2.text = $"经验值: {MenuManager.Instance.playerExperience[1]}";

        nameText1.text = "角色名称: " + MenuManager.Instance.playerName[0];

        nameText2.text = "角色名称: " + MenuManager.Instance.playerName[1];

        levelText1.text = $"等级: {LevelManager.Instance.player1Level}";

        levelText2.text = $"等级: {LevelManager.Instance.player2Level}";
    }

    // void Update()
    // {
    //     if (Time.time > updateTimer)
    //     {
    //         if (MenuManager.Instance != null)
    //         {
    //             expText1.text = $"Exp: {MenuManager.Instance.playerExperience[0]}";
    //             expText2.text = $"Exp: {MenuManager.Instance.playerExperience[1]}";
    //             nameText1.text = "Character Name: " + MenuManager.Instance.playerName[0];
    //             nameText2.text = "Character Name: " + MenuManager.Instance.playerName[1];
    //         }

    //         if (LevelManager.Instance != null)
    //         {
    //             levelText1.text = $"Level: {LevelManager.Instance.player1Level}";
    //             levelText2.text = $"Level: {LevelManager.Instance.player2Level}";
    //         }

    //         updateTimer = Time.time + updateSpeed;
    //     }
    // }

}