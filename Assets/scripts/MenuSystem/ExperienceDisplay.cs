/* 
功能:
显示玩家的经验值和等级。

挂载对象:
应该挂载在一个管理经验值和等级显示的对象上，例如 ExperienceManager。

重要变量:
expText, expText1: 显示玩家经验值的文本对象。
levelText, levelText1: 显示玩家等级的文本对象。
updateSpeed: 更新显示的速度。
_updateTimer: 控制更新频率的计时器。
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI expText;

    [SerializeField] public TextMeshProUGUI expText1;

    [SerializeField] public TextMeshProUGUI levelText;  // 玩家等级显示

    [SerializeField] public TextMeshProUGUI levelText1; // 玩家2等级显示

    [SerializeField] public TextMeshProUGUI nameText;

    [SerializeField] public TextMeshProUGUI nameText1;

    [SerializeField] public float updateSpeed = 0.5f;

    private float _updateTimer;

    void Update()
{
    if (Time.time > _updateTimer)
    {
        if (MenuManager.Instance != null && LevelManager.Instance != null && MenuManager.Instance.playerExperience != null 
        && MenuManager.Instance.playerExperience.Length > 0 && MenuManager.Instance.playerName != null && MenuManager.Instance.playerName.Length > 0)
        {
            if (MenuManager.Instance.playerExperience.Length > 0 && MenuManager.Instance.playerName.Length > 1)
            {
                expText.text = $"Exp: {MenuManager.Instance.playerExperience[0]}";
                expText1.text = $"Exp: {MenuManager.Instance.playerExperience[1]}";

                nameText.text = "Character Name: " + MenuManager.Instance.playerName[3];
                nameText1.text = "Character Name: " + MenuManager.Instance.playerName[4];

                levelText.text = $"Level: {LevelManager.Instance.player1Level}";
                levelText1.text = $"Level: {LevelManager.Instance.player2Level}";
            }
            else
            {
                Debug.LogError("Player experience or name arrays are empty or not properly initialized.");
            }
        }
        else
        {
            Debug.LogError("MenuManager or LevelManager instance is null.");
        }

        _updateTimer = Time.time + updateSpeed;
    }
}

    // void Update()
    // {
    //     if (Time.time > _updateTimer)
    //     {
    //         if (MenuManager.Instance != null && LevelManager.Instance != null)
    //         {
    //             if (MenuManager.Instance.playerExperience.Length > 0 && MenuManager.Instance.playerName.Length > 0)
    //             {
    //                 expText.text = $"Exp: {MenuManager.Instance.playerExperience[0]}";
    //                 expText1.text = $"Exp: {MenuManager.Instance.playerExperience[1]}";

    //                 nameText.text = "Character Name: " + MenuManager.Instance.playerName[0];
    //                 nameText1.text = "Character Name: " + MenuManager.Instance.playerName[1];

    //                 levelText.text = $"Level: {LevelManager.Instance.player1Level}";
    //                 levelText1.text = $"Level: {LevelManager.Instance.player2Level}";
    //             }
    //             else
    //             {
    //                 Debug.LogError("Player experience or name arrays are empty.");
    //             }
    //         }
    //         else
    //         {
    //             Debug.LogError("MenuManager or LevelManager instance is null.");
    //         }

    //         _updateTimer = Time.time + updateSpeed;
    //     }
    // }
}