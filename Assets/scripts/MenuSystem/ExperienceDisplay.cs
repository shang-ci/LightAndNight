using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expText;

    [SerializeField] private TextMeshProUGUI expText1;

    [SerializeField] private float updateSpeed = 0.5f;

    private float _updateTimer;

    void Update()
    {
        if (Time.time > _updateTimer)
        {
            expText.text = $"exp: {MenuManager.Instance.player1Experience}";

            expText1.text = $"exp: {MenuManager.Instance.player2Experience}";

            _updateTimer = Time.time + updateSpeed;
        }
    }
}