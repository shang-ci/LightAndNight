using UnityEngine;

public class BossExperienceManager : MonoBehaviour
{
    public void AllocateExperience(int amount)
    {
        MenuManager.Instance.AddExperienceToPlayer1(amount);

        MenuManager.Instance.AddExperienceToPlayer2(amount);

        Debug.Log($"You gain {amount} exp.");
    }
}