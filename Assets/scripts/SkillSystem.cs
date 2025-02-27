using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
    public Button[] skillButtons;
    public Character selectedCharacter;
    public Skill[] skills;

        public class Character : MonoBehaviour
{
    public new string name = "Player";
    public int currentHP = 100;
    public int maxHP = 100;
    
    public int currentMP = 50;
    public int maxMP = 50;
    
    public int strength = 10;
    public int agility = 15;
}

public class CharacterData : MonoBehaviour
{
    public new string name { get; set; }
    public int hp { get; set; }
    public int maxHp { get; set; }
    public int mp { get; set; }
    public int maxMp { get; set; }
    public int level { get; set; }
    public int experience { get; set; }
    public int nextLevelXP { get; set; }
    
    // 经验条计算比例
    public float GetExperienceRatio() => experience / (float)nextLevelXP;
}
    
    void Start()
    {
        foreach (var btn in skillButtons)
        {
            int index = System.Array.IndexOf(skillButtons, btn);
            btn.onClick.AddListener(() => UseSkill(index));
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

    private void UpdateCharacterUI()
    {
        // Update the character's UI elements here
        Debug.Log("Character UI updated");
    }
}

public abstract class Skill
{
    public string name;
    public int effectValue;
}

public class HealingSkill : Skill {}