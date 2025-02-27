using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    public new string name = "Player";
    public int hp = 100;
    public int maxHp = 100;
    public int mp = 50;
    public int maxMp = 50;
    public int level = 1;
    public int experience = 0;
    public int nextLevelXP = 100;

    public float GetHPRatio() => hp / (float)maxHp;
    public float GetMPRatio() => mp / (float)maxMp;
    public float GetExperienceRatio() => experience / (float)nextLevelXP;
}