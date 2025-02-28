using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBattleController : MonoBehaviour
{
    [Header("战斗配置")]
    [SerializeField] private int bossMaxHP = 100;
    [SerializeField] private int experienceReward = 10;
    
    private int _currentBossHP;

    void Start()
    {
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
        
        if (_currentBossHP <= 0)
        {
            OnBossDefeated();
        }
    }

    private void OnBossDefeated()
    {
        // 发放经验奖励
        MenuManager.Instance.AddExperience(experienceReward);
        
        // 延迟20秒后重启战斗
        Debug.Log("Boss已被击败，20秒后重启战斗。");
        Invoke(nameof(RestartBattle), 20f);

        // 可以在这里添加其他逻辑，比如播放胜利动画、解锁新卡牌等
        //可随意设置条件进行战斗
    }

    private void RestartBattle()
    {
        // 方案1：重新加载场景（适合完全重置）
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        // 方案2：软重启（更高效）
        InitializeBattle();
        FindFirstObjectByType<PlayerHand>().ResetHand(); // 重置玩家手牌
       // 方案2：软重启（更高效）
        Debug.Log("重启战斗。");
        InitializeBattle();
        FindFirstObjectByType<PlayerHand>().ResetHand(); // 重置玩家手牌
        //GetComponent<BossVisuals>().PlayRespawnAnimation(); // Boss重生动画
    }
}