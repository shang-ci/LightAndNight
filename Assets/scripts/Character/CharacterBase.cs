using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [Header("基础属性")]
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;

    [Header("角色信息")]
    public string characterName;
    public int characterID;

    [Header("角色状态")]
    public bool isDead = false;


    [Header("闪光")]
    [SerializeField]private Coroutine flashCoroutine;
    private SpriteRenderer spriteRenderer;


    public virtual void SetCharacterBase(string _name, int _id)
    {
    }

    public virtual void SetCharacterBase(string _name, int _id, CardLibrarySO _library)
    {
    }

    public virtual void SetCharacterBase(PlayerData _playerData)
    {
    }

    public virtual void Awake()
    {
        //cardManager = new PlayerCardManager();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(int damage)
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
        isDead = true;
        EventManager.Instance.TriggerEvent<CharacterBase>("CharacterDied", this);//触发角色死亡事件
    }
}
