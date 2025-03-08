using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuController1 : MonoBehaviour
{
    public GameObject detailPanel;

    private GamePlayControls inputActions;

    private bool _isProcessingInput;

    void Awake()
    {
        // 初始化输入系统
        inputActions = new GamePlayControls();

        // 绑定两个动作到对应方法
        inputActions.UI.ToggleDetail.performed += _ => ToggleDetail();
    }

    void Update()
    {
        // 检测键盘（M键）和手柄（X键，通常是JoystickButton2）
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            ToggleDetail();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !_isProcessingInput)
    {
        _isProcessingInput = true;
        ToggleDetail();
        _isProcessingInput = false;
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

        //强制设置为true状态
        // detailPanel.SetActive(true);

        // Debug.Log("强制激活，版面状态: " + detailPanel.activeSelf);
    
    }
}
