using System.Collections.Generic;
using UnityEngine;


public abstract class StatusEffect : ScriptableObject
{
    public string effectName;
    public int value;
    public StatusEffectTargetType targetType;
    public int round;//�����غ���
    public EffectTiming timing;//Ŀǰ��˵������е�Ч��һ��ʱ���Ϳ�����


    //�ı�ʱ�������ڴ������ʱ����״̬Ч����״̬�������ڴ˴��ı�ʱ��
    public abstract void ChangeTime(CharacterBase character);
    public abstract void ExecuteEffect(CharacterBase from, CharacterBase target);
    public abstract void ExecuteEffect(CharacterBase from, List<CharacterBase> targets);
    public abstract void RemoveEffect(CharacterBase character);
}
