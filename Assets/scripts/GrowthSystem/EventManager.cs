using UnityEngine;

using System;
public class EventManager : MonoBehaviour
{
    public static event Action OnBossDefeated;

    public static void BossDefeated()
    {
        OnBossDefeated?.Invoke();
    }
}