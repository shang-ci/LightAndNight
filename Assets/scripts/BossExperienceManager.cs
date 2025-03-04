using UnityEngine;

public class BossExperienceManager : MonoBehaviour
{
    public void AllocateExperience(int amount)
    {
        MenuManager.Instance.AddExperience(amount);
        Debug.Log($"You gain {amount} exp.");
    }
}