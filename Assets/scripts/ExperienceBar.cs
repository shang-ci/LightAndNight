using UnityEngine.UI;
using UnityEngine;

public class ExperienceBar : MonoBehaviour
{
    public Slider expSlider;
    public Text levelText;
    
    public void SetExperience(int currentExp, int nextLevelExp)
    {
        expSlider.maxValue = nextLevelExp;
        expSlider.value = currentExp;
        
        levelText.text = "Level " + (currentExp >= nextLevelExp ? 
            (int)(currentExp / nextLevelExp) : (int)(currentExp / nextLevelExp));
    }
}