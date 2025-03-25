using UnityEngine;

public class DamageEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if(target == null) return;

        var damage = value;
        target.TakeDamage(damage);
        Debug.Log($"执行了{damage}点伤害！");
    }  
}

