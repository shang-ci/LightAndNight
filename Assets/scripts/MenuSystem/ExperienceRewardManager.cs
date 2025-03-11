/*
功能:
管理经验奖励的分配。

挂载对象:
应该挂载在一个全局管理对象上，例如 GameManager。

重要变量:
无特别重要的变量，主要是方法 RewardExperience 用于分配经验值。
 */

using UnityEngine;

public class ExperienceRewardManager : MonoBehaviour
{
    public void RewardExperience(int[] playerExp)
    {
        for (int i = 0; i < playerExp.Length; i++)
        {
            MenuManager.Instance.AddExperienceToPlayer(i, playerExp[i]);
            Debug.Log($"Player {i + 1} 获得 {playerExp[i]} 点经验");
        }
    }
}