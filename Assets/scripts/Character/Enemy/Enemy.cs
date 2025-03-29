using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionDataSO;
    public EnemyAction currentAction;

    [Header("卡牌相关")]
    public CardLibrarySO library;//卡牌库
    public EnemyCardManger cardManger;
    public Transform cardParent;//抽出来的卡牌的位置的初始位置


    [Header("攻击目标")]
    public List<CharacterBase> targets;
    public CharacterBase target;
    protected Player player;


    public override void SetCharacterBase(string _name, int _id)
    {
        base.SetCharacterBase(_name, _id);
        this.maxHP = 100;
        this.currentHP = 100;
        this.characterName = _name;
        this.characterID = _id;
    }

    public override void Awake()
    {
        base.Awake();
        cardManger = new EnemyCardManger(library, this,cardParent);
        cardManger.InitializeDeck();
        SetCharacterBase("enemy1", 102);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener("EnemyTurnBegin", OnEnemyTurnBegin);//敌人回合开始——执行卡牌效果
        EventManager.Instance.AddListener<object>("EnemyUseCard", cardManger.DiscardCard);//打出卡牌后——弃掉
        //EventManager.Instance.AddListener("EnemyTurnEnd", cardManger.DisAllHandCardsOnEnemyTurnEnd);//敌人回合结束——弃掉所有手牌——有卡牌自己处理
        EventManager.Instance.AddListener("PlayerTurnBegin", cardManger.NewTurnDrawCards);//玩家回合开始——抽牌
        EventManager.Instance.AddListener<int>("EnemyDrawCard", cardManger.DrawCard);//抽牌——卡牌能力
        //EventManager.Instance.AddListener("GameStart", cardManger.);//战斗开始，加载场景
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener("EnemyTurnBegin", OnEnemyTurnBegin);
        EventManager.Instance.RemoveListener<object>("EnemyUseCard", cardManger.DiscardCard);
        //EventManager.Instance.RemoveListener("EnemyTurnEnd", cardManger.DisAllHandCardsOnEnemyTurnEnd);
        EventManager.Instance.RemoveListener("PlayerTurnBegin", cardManger.NewTurnDrawCards);
        EventManager.Instance.RemoveListener<int>("EnemyDrawCard", cardManger.DrawCard);
        //EventManager.Instance.RemoveListener("GameStart", cardManger.);
    }


    #region 意图相关
    //产生意图——从actions里根据触发率选择意图——不过这里只能有一个意图，想实现多个效果，可以在这里加一个数组/制作复合的effect
    public virtual void OnPlayerTurnBegin()
    {
        //计算所有动作的总概率
        float totalProbability = 0f;
        foreach (var action in actionDataSO.actions)
        {
            totalProbability += action.probability;//这样就不必保证概率总和为1
        }

        //0到totalProbability的随机数
        float randomPoint = Random.value * totalProbability;

        //选择与randomPoint相匹配的action
        float cumulativeProbability = 0f; // 累积概率
        foreach (var action in actionDataSO.actions)
        {
            cumulativeProbability += action.probability;
            if (randomPoint < cumulativeProbability)
            {
                // 如果randomPoint在当前动作的概率区间内，选择当前动作
                currentAction = action;
                break;
            }
        }

        // 保存原始值
        //currentAction.originalValue = currentAction.effect.value;

        //精准&&暴击判定
        if (Random.value <= currentAction.accuracy)
        {
            currentAction.effect.value = (int)(currentAction.effect.value * currentAction.criticalRate); //暴击
        }
        else
        {
            Debug.Log(" 不暴击");
        }

        //放在这里获得是因为在Start中可能还没有player，这样就可以保证player不为空
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    //执行意图——在敌人回合开始时执行——点击回合转换按钮时敌人回合开始
    public virtual void OnEnemyTurnBegin()
    {
        foreach(var card in cardManger.handCards)
        {
            foreach (var effect in card.cardDataSO.effects)//遍历卡牌效果——这样在有多个效果时也能执行
            {
                switch(effect.targetType)
                {
                    case EffectTargetType.Self:
                        target = this;
                        break;
                    case EffectTargetType.Target:
                        target = GameManager.instance.playerRandomCharacter;
                        break;
                    case EffectTargetType.Our:
                        targets = GameManager.instance.enemyCharacters;
                        break;
                    case EffectTargetType.Enemies:
                        targets = GameManager.instance.playerCharacters;
                        break;
                    case EffectTargetType.ALL:
                        targets = GameManager.instance.allCharacters;
                        break;
                    case EffectTargetType.Random:
                        target = GameManager.instance.randomCharacter;
                        break;
                }
                if (targets != null)
                    card.ExecuteCardEffects(this, targets);
                else if (target != null)
                    card.ExecuteCardEffects(this, target);

                target = null;
                targets = null;
            }
        }
        cardManger.handCards.Clear();
    }

    public virtual void Skill()
    {
        // animator.SetTrigger("skill");
        // currentAction.effect.Execute(this, this);
        StartCoroutine(ProcessDelayAction("skill"));
    }

    public virtual void Attack()
    {
        // animator.SetTrigger("attack");
        // currentAction.effect.Execute(this, player);
        StartCoroutine(ProcessDelayAction("attack"));
    }

    //执行行为的暴击部分——处理延迟动作——在动画播放到一定程度时执行——避免UI更新和动画播放不同步；其实也可以在动画片段中加入事件
    IEnumerator ProcessDelayAction(string actionName)
    {
        //animator.SetTrigger(actionName);
        //// 等到动画播放了 60% 并且不是在 layer0 的转换过程中 并且动画名是 actionNmae，才往后执行
        //yield return new WaitUntil(() => (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f) && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName(actionName));
        yield return new WaitForSeconds(0f);

        if (actionName == "attack")
        {
            currentAction.effect.Execute(this, player);
        }
        else
        {
            currentAction.effect.Execute(this, this);
        }
    }
    #endregion

    //public override void ExecuteStatusEffects(EffectTiming timing)
    //{
    //    base.ExecuteStatusEffects(timing);
    //    foreach (var effect in activeEffects)
    //    {
    //        if (effect.timing == timing)
    //        {
    //            effect.ExecuteEffect(this, player);
    //        }
    //    }
    //}


    //集合遍历时被修改会触发InvalidOperationException异常，所以这里使用for循环/先记录要删除的元素后面再foreach删除
    //敌人的状态效果回合数更新——这里就不用事件来调用了，要是敌人数量很多那岂不是要给每个敌人都添加一个事件监听太费事了
    //public override void UpdateStatusEffectRounds()
    //{
    //    base.UpdateStatusEffectRounds();
    //    foreach (var statusEffect in activeEffects)
    //    {
    //        statusEffect.round--;
    //        if (statusEffect.round <= 0)
    //        {
    //            statusEffect.RemoveEffect(this);
    //        }
    //    }
    //}
    //public override void UpdateStatusEffectRounds()
    //{
    //    base.UpdateStatusEffectRounds();
    //    for (int i = activeEffects.Count - 1; i >= 0; i--)
    //    {
    //        StatusEffect statusEffect = activeEffects[i];
    //        statusEffect.round--;
    //        if (statusEffect.round <= 0)
    //        {
    //            statusEffect.RemoveEffect(this);
    //        }
    //    }
    //}
}
