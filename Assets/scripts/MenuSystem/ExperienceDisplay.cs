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
    [SerializeField] private TextMeshProUGUI expText;

    [SerializeField] private TextMeshProUGUI expText1;

    [SerializeField] private TextMeshProUGUI levelText;  // 玩家等级显示

    [SerializeField] private TextMeshProUGUI levelText1; // 玩家2等级显示

    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI nameText1;

    [SerializeField] private float updateSpeed = 0.5f;

    private float _updateTimer;

    void Update()
    {
        if (Time.time > _updateTimer)
        {
            expText.text = $"Exp: {MenuManager.Instance.playerExperience[0]}";

            expText1.text = $"Exp: {MenuManager.Instance.playerExperience[1]}";

            nameText.text = "Character Name: " + MenuManager.Instance.playerName[0]; // 使用 Instance

            nameText1.text = "Character Name: " + MenuManager.Instance.playerName[1]; // 使用 Instance

            levelText.text = $"Level: {LevelManager.Instance.player1Level}";
            //Debug.Log($"日志console已更新level值:{LevelManager.Instance.player1Level}");

            levelText1.text = $"Level: {LevelManager.Instance.player2Level}";
            
            _updateTimer = Time.time + updateSpeed;
        }
    }
}




// using TMPro;
// using UnityEngine;

// public class ExperienceDisplay : MonoBehaviour
// {
//     [Header("UI Elements")]
//     [SerializeField] private TextMeshProUGUI[] expTexts = new TextMeshProUGUI[2];
//     [SerializeField] private TextMeshProUGUI[] levelTexts = new TextMeshProUGUI[2];
//     [SerializeField] private TextMeshProUGUI[] nameTexts = new TextMeshProUGUI[2];
    
//     [SerializeField] private float updateSpeed = 0.5f;
//     private float _updateTimer;

//     void Update()
//     {
//         if (Time.time > _updateTimer)
//         {
//             UpdateAllPlayerUI();
//             _updateTimer = Time.time + updateSpeed;
//         }
//     }

//     private void UpdateAllPlayerUI()
//     {
//         for(int i = 0; i < 2; i++)
//         {
//             UpdatePlayerUI(i);
//         }
//     }

//     private void UpdatePlayerUI(int playerIndex)
//     {
//         if(playerIndex >= DataManager.Instance.players.Length) return;

//         var player = DataManager.Instance.players[playerIndex];
        
//         if(expTexts[playerIndex])
//             expTexts[playerIndex].text = $"Exp: {player.experience}";
        
//         if(levelTexts[playerIndex])
//             levelTexts[playerIndex].text = $"Level: {player.level}";
        
//         if(nameTexts[playerIndex])
//             nameTexts[playerIndex].text = $"Name: {player.playerName}";
//     }

//     // 初始化时调用（确保场景加载时立即刷新）
//     private void Start()
//     {
//         UpdateAllPlayerUI();
//     }
// }