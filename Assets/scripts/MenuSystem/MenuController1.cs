using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController1 : MonoBehaviour
{
    public GameObject detailPanel;

    private GamePlayControls inputActions;

    void Awake()
    {
        // 初始化输入系统
        inputActions = new GamePlayControls();

        // 绑定两个动作到对应方法
        inputActions.UI.ToggleDetail.performed += _ => ToggleDetail();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            ToggleDetail();
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

    public void ToggleDetail()
    {
        detailPanel.SetActive(!detailPanel.activeSelf);
        Debug.Log("版面状态: " + detailPanel.activeSelf);
    }
}
