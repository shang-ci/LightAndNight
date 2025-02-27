using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Action onMenuToggle;
    public Action onSkillConfirm;
    
    void Awake()
    {
        // 键盘绑定
        InputAction menuToggleAction = new InputAction("MenuToggle");
        menuToggleAction.AddBinding("<Keyboard>/escape");
        menuToggleAction.performed += context => onMenuToggle.Invoke();
        
        // 手柄绑定
        InputAction skillConfirmAction = new InputAction("SkillConfirm");
        skillConfirmAction.AddBinding("<Gamepad>/buttonSouth");
        skillConfirmAction.performed += context => onSkillConfirm.Invoke();
        
        // 其他输入...
    }
}