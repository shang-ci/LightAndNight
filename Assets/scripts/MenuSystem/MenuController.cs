using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;

    private GamePlayControls inputActions;

    public TextMeshProUGUI expText; // 经验值文本

    public TextMeshProUGUI expText1; // 经验值文本

    public TextMeshProUGUI nameText;

    public TextMeshProUGUI nameText1;

    public TextMeshProUGUI levelText;  // 玩家等级显示

    public TextMeshProUGUI levelText1; // 玩家2等级显示

    private LevelManager levelManager;  // 引用LevelManager实例

    void Awake()
    {
        // 初始化输入系统
        inputActions = new GamePlayControls();

        // 绑定两个动作到对应方法
        inputActions.UI.ToggleMenu.performed += _ => ToggleMenu();

        // 初始化LevelManager实例
        levelManager = Object.FindFirstObjectByType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found in the scene!");
        }
    }

    void Update()
    {
        // 检查 levelManager 是否为 null
        if (levelManager == null)
        {
            Debug.LogError("LevelManager is not assigned!");
            return; // 如果为 null，提前退出
        }

        // 检测键盘（Esc）和手柄（Start键，通常是JoystickButton7）
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            ToggleMenu();
        }

        int player1Level = levelManager.GetLevel1Experience(MenuManager.Instance.player1Experience, levelManager.player1Level, levelManager.expperlevel1);
        int player2Level = levelManager.GetLevel2Experience(MenuManager.Instance.player2Experience, levelManager.player2Level, levelManager.expperlevel2);

        // 更新经验值显示（仅在菜单打开时更新）
        if (menuPanel.activeSelf)
        {
            expText.text = "Exp: " + MenuManager.Instance.GetPlayer1Experience(); // 使用 Instance

            expText1.text = "Exp: " + MenuManager.Instance.GetPlayer2Experience(); // 使用 Instance

            nameText.text = "Character Name: " + "Osborn";

            nameText1.text = "Character Name: " + "Evan";

            levelText.text = "Level: " + player1Level; // 使用计算出的等级

            levelText1.text = "Level: " + player2Level; // 使用计算出的等级

            Debug.Log("已更新level值");
        }
    }

    void OnEnable()
    {
        inputActions.UI.Enable(); // 启用UI Action Map
    }

    void OnDisable()
    {
        inputActions.UI.Disable(); // 禁用UI Action Map
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        //Debug.Log("菜单状态: " + menuPanel.activeSelf);
    }
}
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class MenuController : MonoBehaviour
// {
//     public GameObject menuPanel; // 菜单面板

//     public TextMeshProUGUI expText; // 经验值文本

//     // public Text HP;        // 血条

//     // public Text MP;       // 经验值

//     // public Text Level;     // 等级

//     void Update()
//     {
//         // 检测键盘（Esc）和手柄（Start键，通常是JoystickButton7）
//         if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
//         {
//             ToggleMenu();
//         }

//         // 更新经验值显示（仅在菜单打开时更新）
//         if (menuPanel.activeSelf)
//         {
//             expText.text = "MP: " + MenuManager.Instance.GetExperience();
//         }
//     }

//     // 切换菜单显示状态
//     public void ToggleMenu()
//     {
//         menuPanel.SetActive(!menuPanel.activeSelf);
//     }
// }