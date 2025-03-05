using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;

    public GameObject detailPanel;

    private GamePlayControls inputActions;

    public TextMeshProUGUI expText; // 经验值文本

    public TextMeshProUGUI expText1; // 经验值文本

    public TextMeshProUGUI nameText;

    public TextMeshProUGUI nameText1;

    void Awake()
    {
        // 初始化输入系统
        inputActions = new GamePlayControls();

        // 绑定两个动作到对应方法
        inputActions.UI.ToggleMenu.performed += _ => ToggleMenu();
        inputActions.UI.ToggleDetail.performed += _ => ToggleDetail();
    }

    void Update()
    {
        // 检测键盘（Esc）和手柄（Start键，通常是JoystickButton7）
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            ToggleMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            ToggleDetail();
        }

        // 更新经验值显示（仅在菜单打开时更新）
        if (menuPanel.activeSelf)
        {
            expText.text = "Exp: " + MenuManager.Instance.GetPlayer1Experience(); // 使用 Instance
            
            expText1.text = "Exp: " + MenuManager.Instance.GetPlayer2Experience(); // 使用 Instance


            nameText.text = "Character Name: " + "Evan";

            nameText1.text = "Character Name: " + "Osborn";
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

    public void ToggleDetail()
    {
        detailPanel.SetActive(!detailPanel.activeSelf);
        //Debug.Log("版面状态: " + detailPanel.activeSelf);
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