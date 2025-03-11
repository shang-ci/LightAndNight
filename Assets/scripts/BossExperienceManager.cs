/* 
功能:
分配玩家在击败Boss后获得的经验值。

挂载对象:
应该挂载在一个管理Boss战斗的对象上，例如 BossManager。

重要变量:
无特别重要的变量，主要是方法 AllocateExperience 用于分配经验值。
 */
using UnityEngine;

public class BossExperienceManager : MonoBehaviour
{
    public void AllocateExperience(int[] playerExp)
    {
        for (int i = 0; i < playerExp.Length; i++)
        {
            MenuManager.Instance.AddExperienceToPlayer(i, playerExp[i]);
            Debug.Log($"Player {i + 1} 获得 {playerExp[i]} 点经验");
        }
    }
}