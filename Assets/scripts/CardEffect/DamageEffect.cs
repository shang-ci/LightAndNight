using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Effect/Card/DamageEffect")]
public class DamageEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if(target == null) return;

        var damage = value;
        target.TakeDamage(damage);
        Debug.Log($"执行了{damage}点伤害！");
    }

    public override void Execute(CharacterBase from, List<CharacterBase> targets)
    {
        
    }
}

