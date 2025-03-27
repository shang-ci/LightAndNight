using UnityEngine;

public enum EffectTargetType
{
    Target,
    All,
    Others
}

public class DamageEffect : MonoBehaviour
{
    public int value; // Add this line to define the value variable
    public EffectTargetType targetType; // Add this line to define the targetType variable
    public void Execute(CharacterBaseNew from, CharacterBaseNew target)
    {
        if(target == null) return;
        
        switch (targetType)
        {
            case EffectTargetType.Target:
            var damage = (int)Mathf.Round(value * from.baseStrength);
                target.TakeDamage(damage);
                Debug.Log($"执行了{damage}点伤害！");
                break;
            case EffectTargetType.All:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBaseNew>().TakeDamage(value);
                }
                break;
            case EffectTargetType.Others:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<CharacterBaseNew>() != target)
                    {
                        enemy.GetComponent<CharacterBaseNew>().TakeDamage(value);
                    }
                }
                break;
        }
    }
    
}

