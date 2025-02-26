using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Action onMenuToggle;
    public Action onSkillConfirm;
    
    void Awake()
    {
        // 键盘绑定
        InputAction menuToggleAction = new InputAction("MenuToggle");
        menuToggleAction.binding = "<Escape>";
        menuToggleAction.performed += context => onMenuToggle.Invoke();
        
        // 手柄绑定
        InputAction skillConfirmAction = new InputAction("SkillConfirm");
        skillConfirmAction.binding = "<Submit>";
        skillConfirmAction.performed += context => onSkillConfirm.Invoke();
        
        // 其他输入...
    }
}