using UnityEngine;

public class Player : CharacterBase
{
    public IntVariable playerMana;

    public int maxMana;

    public int CurrentMana { get => playerMana.currentValue; set => playerMana.SetValue(value); }

    private void OnEnable()
    {
        playerMana.maxValue = maxMana;

        CurrentMana = playerMana.maxValue;  //设置初始法力值
    }
}
