using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : Item
{
    public int value;
    public EffectTargetType targetType;

    // ִ��Ч��
    public virtual void Execute(CharacterBase from, CharacterBase target) { }
    public virtual void Execute(CharacterBase from, List<CharacterBase> targets) { }


    public virtual void Initialize(int value, EffectTargetType targetType)
    {
        this.value = value;
        this.targetType = targetType;
    }
}