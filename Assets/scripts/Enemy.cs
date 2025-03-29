using System.Collections;
using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionDataSO;
    public EnemyAction currentAction;

    public CardLibrarySO library;//���ƿ�
    public EnemyCardManger cardManger;
    public Transform cardParent;//������Ŀ��Ƶ�λ�õĳ�ʼλ��

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
        SetCharacterBase("enemy1", 102);
    }

    //������ͼ������actions����ݴ�����ѡ����ͼ������������ֻ����һ����ͼ����ʵ�ֶ��Ч���������������һ������/�������ϵ�effect
    public virtual void OnPlayerTurnBegin()
    {
        //�������ж������ܸ���
        float totalProbability = 0f;
        foreach (var action in actionDataSO.actions)
        {
            totalProbability += action.probability;//�����Ͳ��ر�֤�����ܺ�Ϊ1
        }

        //0��totalProbability�������
        float randomPoint = Random.value * totalProbability;

        //ѡ����randomPoint��ƥ���action
        float cumulativeProbability = 0f; // �ۻ�����
        foreach (var action in actionDataSO.actions)
        {
            cumulativeProbability += action.probability;
            if (randomPoint < cumulativeProbability)
            {
                // ���randomPoint�ڵ�ǰ�����ĸ��������ڣ�ѡ��ǰ����
                currentAction = action;
                break;
            }
        }

        // ����ԭʼֵ
        //currentAction.originalValue = currentAction.effect.value;

        //��׼&&�����ж�
        if (Random.value <= currentAction.accuracy)
        {
            currentAction.effect.value = (int)(currentAction.effect.value * currentAction.criticalRate); //����
        }
        else
        {
            Debug.Log(" ������");
        }

        //��������������Ϊ��Start�п��ܻ�û��player�������Ϳ��Ա�֤player��Ϊ��
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    //ִ����ͼ�����ڵ��˻غϿ�ʼʱִ�С�������غ�ת����ťʱ���˻غϿ�ʼ
    public virtual void OnEnemyTurnBegin()
    {
        switch (currentAction.effect.targetType)
        {
            case EffectTargetType.Self:
                Skill();
                break;
            case EffectTargetType.Target:
                Attack();
                break;
            case EffectTargetType.ALL:
                break;
        }
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

    //ִ����Ϊ�ı������֡��������ӳٶ��������ڶ������ŵ�һ���̶�ʱִ�С�������UI���ºͶ������Ų�ͬ������ʵҲ�����ڶ���Ƭ���м����¼�
    IEnumerator ProcessDelayAction(string actionName)
    {
        //animator.SetTrigger(actionName);
        //// �ȵ����������� 60% ���Ҳ����� layer0 ��ת�������� ���Ҷ������� actionNmae��������ִ��
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


    //���ϱ���ʱ���޸Ļᴥ��InvalidOperationException�쳣����������ʹ��forѭ��/�ȼ�¼Ҫɾ����Ԫ�غ�����foreachɾ��
    //���˵�״̬Ч���غ������¡�������Ͳ����¼��������ˣ�Ҫ�ǵ��������ܶ�������Ҫ��ÿ�����˶����һ���¼�����̫������
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
