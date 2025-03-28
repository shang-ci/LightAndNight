using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    public string characterName;
    public int characterID;

    //public List<CardDataSO> cardDatas;
    //public List<Card> handCards;

    [SerializeField]private Coroutine flashCoroutine;
    private SpriteRenderer spriteRenderer;


    public virtual void SetCharacterBase(string _name)
    {
    }

    public virtual void Awake()
    {
        //cardManager = new PlayerCardManager();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if(currentHP < damage)
        {
            currentHP = 0;
            Died();
        }
        currentHP -= damage;
    }


    // 启动闪烁效果
    public void StartFlashing()
    {
        if (flashCoroutine == null)
        {
            flashCoroutine = StartCoroutine(Flash());
        }
    }

    // 停止闪烁效果
    public void StopFlashing()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
            spriteRenderer.color = Color.white; // 恢复原始颜色
        }
    }

    // 闪烁协程
    private IEnumerator Flash()
    {
        while (true)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public virtual void Died()
    {
        Debug.Log(this.characterName + "Died");
    }
}
