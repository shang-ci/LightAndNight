/* 
功能:
控制Boss战斗的逻辑，包括Boss的生命值和经验奖励。

挂载对象:
应该挂载在一个管理Boss战斗的对象上，例如 BossManager。

重要变量:
bossMaxHP: Boss的最大生命值。
experienceReward, experienceReward1: 击败Boss后玩家获得的经验值。
_currentBossHP: Boss当前的生命值。
experienceRewardManager: 引用经验奖励管理器的实例。
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossBattleController : MonoBehaviour
{
    [Header("战斗配置")]
    [SerializeField] private int bossMaxHP;
    [SerializeField] private int experienceReward;

    [SerializeField] private int experienceReward1;

    private int _currentBossHP;

    private ExperienceRewardManager experienceRewardManager; // 新增引用

    void Start()
    {
        experienceRewardManager = FindFirstObjectByType<ExperienceRewardManager>(); // 查找 ExperienceRewardManager 脚本实例
        if (experienceRewardManager == null)
        {
            Debug.LogError("ExperienceRewardManager not found in the scene!");
        }

        InitializeBattle();
    }

    private void InitializeBattle()
    {
        _currentBossHP = bossMaxHP;

        Debug.Log("Boss战开始！");
    }

    public void TakeDamage(int damage)
    {
        _currentBossHP = Mathf.Max(0, _currentBossHP - damage);

        if (_currentBossHP > -1000)
        {
            OnBossDefeated();
        }
    }

    private void OnBossDefeated()
    {
        int[] playerExp = { experienceReward, experienceReward1 };
        experienceRewardManager.RewardExperience(playerExp); // 为两个玩家分配经验

    }


    private void RestartBattle()
    {
        Debug.Log("重启战斗。");
        InitializeBattle();
    }
}