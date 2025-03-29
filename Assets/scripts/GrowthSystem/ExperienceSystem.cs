using UnityEngine;
using EventSystem;
public class ExperienceSystem : MonoBehaviour
{
    void Start()
    {
        EventManager.OnBossDefeated += DistributeExperience;

    }

    private void DistributeExperience()
    {
        Debug.Log("DistributeExperience called.");

        foreach (var card in FindObjectsByType<Card>(FindObjectsSortMode.None))
        {
            card.GainExperience(10);

            Debug.Log("gained 10 experience.");
        }
    }
}
