using UnityEngine.UI;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public class Character : MonoBehaviour
{
    public string name = "Player";
    public int currentHP = 100;
    public int maxHP = 100;
    
    public int currentMP = 50;
    public int maxMP = 50;
    
    public int strength = 10;
    public int agility = 15;
}
    public Text nameText;
    public Slider hpSlider;
    public Slider mpSlider;
    public Text strengthText;
    public Text agilityText;
    
    public void UpdateStats(Character character)
    {
        nameText.text = character.name;
        hpSlider.value = character.currentHP / character.maxHP;
        mpSlider.value = character.currentMP / character.maxMP;
        
        strengthText.text = $"Strength: {character.strength}";
        agilityText.text = $"Agility: {character.agility}";
    }
}