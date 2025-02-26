using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject skillPanel;
    public GameObject inventoryPanel;
    
    private bool isMenuOpen = false;
    
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        mainMenuPanel.SetActive(isMenuOpen);
        
        // 处理输入焦点
        if (isMenuOpen)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
    
    // 输入处理
    void Update()
    {
        // 键盘快捷键
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
        
        // 手柄输入（示例）
        if (Input.GetButtonDown("Submit")) // 手柄确认键
        {
            // 处理技能/物品选择
        }
        
    }
}