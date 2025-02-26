using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
    public Button[] skillButtons;
    public Character selectedCharacter;
    
    void Start()
    {
        foreach (var btn in skillButtons)
        {
            btn.onClick.AddListener(UseSkill);
        }
    }
    
    public void UseSkill(int skillIndex)
    {
        Skill skill = skills[skillIndex];
        if (skill is HealingSkill)
        {
            selectedCharacter.currentHP += skill.effectValue;
            UpdateCharacterUI();
        }
        // 添加冷却时间逻辑
    }
}

public abstract class Skill
{
    public string name;
    public int effectValue;
}

public class HealingSkill : Skill {}