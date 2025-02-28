using UnityEngine;
using UnityEngine.UI;

public class AttributeMenuController : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;
    public Slider mpSlider;
    public Text levelText;
    //public ProgressBar experienceBar;
    public Text expText;
    
    public CharacterDataInfo selectedCharacter;
    
    public void BindCharacter(CharacterDataInfo characterDataInfo)
    {
        selectedCharacter = characterDataInfo;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        nameText.text = selectedCharacter.name;
        hpSlider.value = selectedCharacter.GetHPRatio();
        mpSlider.value = selectedCharacter.GetMPRatio();
        levelText.text = $"Level: {selectedCharacter.level}";
        //experienceBar.progress = selectedCharacter.GetExperienceRatio();
        expText.text = $"{selectedCharacter.experience}/{selectedCharacter.nextLevelXP}";
    }
}