using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    private GamePlayControls inputActions;

    public TextMeshProUGUI expText; // 经验值文本

    void Awake()
    {
        // 初始化输入系统
        inputActions = new GamePlayControls();
        // 绑定ToggleMenu事件
        inputActions.UI.ToggleMenu.performed += ctx => ToggleMenu();
    }

    void Update()
    {
        // 检测键盘（Esc）和手柄（Start键，通常是JoystickButton7）
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            ToggleMenu();
        }

        // 更新经验值显示（仅在菜单打开时更新）
        if (menuPanel.activeSelf)
        {
            expText.text = "MP: " + MenuManager.Instance.GetExperience();
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