using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expText;

    [SerializeField] private TextMeshProUGUI expText1;

    [SerializeField] private TextMeshProUGUI levelText;  // 玩家等级显示

    [SerializeField] private TextMeshProUGUI levelText1; // 玩家2等级显示

    [SerializeField] private float updateSpeed = 0.5f;

    private float _updateTimer;

    void Update()
    {
        if (Time.time > _updateTimer)
        {
            expText.text = $"exp: {MenuManager.Instance.player1Experience}";

            expText1.text = $"exp: {MenuManager.Instance.player2Experience}";

            levelText.text = $"Level: {MenuManager.Instance.player1Level}";

            levelText1.text = $"Level: {MenuManager.Instance.player2Level}";
            
            _updateTimer = Time.time + updateSpeed;
        }
    }
}