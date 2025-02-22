using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public int value;

    //public EffectTargetType targetType;

    public abstract void Execute(CharacterBaseNew from,CharacterBaseNew target);
}
