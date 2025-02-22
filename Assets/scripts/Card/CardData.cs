using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Data", order = 51)]
public class CardData : ScriptableObject
{
    public string cardName;
    public int damage;
    public Sprite cardSprite;
}