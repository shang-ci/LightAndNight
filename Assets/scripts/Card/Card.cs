using EventSystem;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

[System.Serializable]
public class Card : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler,IPointerEnterHandler, IPointerExitHandler
{
    [Header("卡牌属性")]
    public CardDataSO cardDataSO; // 卡牌数据
    public int damage;      // 伤害值


    [Header("组件")]
    public SpriteRenderer cardSprite;
    public TextMeshPro costText;
    public TextMeshPro descriptionText;
    public TextMeshPro typeText;
    public TextMeshPro cardName;
    public CharacterBase owner; // 卡牌拥有者

    [Header("拖拽箭头")]
    public GameObject arrowPrefab;
    private GameObject currentArrow;

    [Header("拖拽执行状态")]
    private bool canMove;//是否可以拖拽，当尝试拖拽时，如果卡牌是攻击类型，就会生成箭头，如果是技能或能力类型，就可以拖拽
    private bool canExecute;//是否可以执行，当拖拽结束时，如果鼠标在敌人上，就可以执行；如果是针对玩家自己的技能或能力，只要超过1f就可以执行，就是说只要拖拽到屏幕上方y轴大于1f就可以执行
    public bool ownerIsEnemy;//判断是否可以拖拽

    [Header("攻击目标")]
    public List<CharacterBase> targets;
    public CharacterBase target;


    [Header("卡牌的位置&上提动画")]
    // 卡牌原始位置——鼠标选中时，卡牌会上移，所以需要记录原始位置
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public int originalLayerOrder;// 卡牌原始层级
    public bool isAnimating;//是否正在移动中/抽卡的动画中——若是在移动就不能让它有上移效果
    public bool isAvailiable = true;//判断player的当前能量是否足够使用这张卡


    public string id; // 对应Excel中的卡牌ID
    public int currentLevel = 1;
    public int currentExp = 0;
    public float[] attributes = new float[6]; // 六个属性值


    public void InitCard(CardDataSO cardData, CharacterBase _owner)
    {
        cardDataSO = cardData;

        //使用carddata的副本初始化卡牌的数据——这样不论是第一次抽取这张卡还是怎样都使用
        cardSprite.sprite = cardData.itemIcon;
        costText.text = cardData.cost.ToString();
        descriptionText.text = cardData.itemDescription;
        typeText.text = cardData.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "技能",
            CardType.Abilities => "能力",
            _ => throw new System.NotImplementedException(),
        };
        cardName.text = cardData.itemName;

        owner = _owner;
        ownerIsEnemy = owner is Enemy;
        target = null;
        targets = null;
    }

    // 经验增长处理
    public void GainExperience(int exp)
    {
        currentExp += exp;
        CheckUpgrade();
    }

    private void CheckUpgrade()
    {
        if (currentExp < GetRequiredExp(currentLevel + 1)) return;

        // 升级处理
        currentLevel++;
        currentExp -= GetRequiredExp(currentLevel); 

        // 应用属性倍数
        var levelData = GetLevelData(currentLevel);
        ApplyAttributes(levelData);

        // 通知UI更新
        //AttributePanel.instance.UpdateCard(this);
    }

    // 获取升级所需经验
    private int GetRequiredExp(int targetLevel)
    {
        return ExistingExcelHelper.cardExperienceList
            .Find(e => e.level == targetLevel).requiredExp;
    }

    // 获取当前等级的属性倍数
    private CardGrowthConfig GetLevelData(int level)
    {
        return ExistingExcelHelper.cardLevelDataList
            .Find(ld => ld.cardId == id && ld.level == level);
    }

    // 应用属性倍数
    private void ApplyAttributes(CardGrowthConfig data)
    {
        for (int i = 0; i < 6; i++)
        {
            attributes[i] *= data.attributeMults[i];
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //TOOD：能量是否足够释放卡牌
        if (!isAvailiable || ownerIsEnemy)
        {
            return;
        }
        isAnimating = true;//防止卡牌在拖拽时有上提的动画效果
        Debug.Log("可以拖拽");

        switch (cardDataSO.cardType)
        {
            // 如果是攻击类型，就生成箭头
            case CardType.Attack:
                currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                break;

            //如果是技能或能力类型，就可以拖拽
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
        }
    }

    //单个目标时由自己选，多个目标时由gamemanager选
    public void OnDrag(PointerEventData eventData)
    {
        if (!isAvailiable || ownerIsEnemy)
        {
            return;
        }

        if (canMove)//拖拽&&执行目标——随机目标/多个目标
        {
            

            // 将鼠标屏幕坐标转换为世界坐标——实现卡牌跟随鼠标移动拖拽效果
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = worldPos;

            //根据卡牌的效果类型，来确定effect实施的目标
            if (cardDataSO.effects != null)
            {
                foreach (var effect in cardDataSO.effects)
                {
                    switch (effect.targetType)
                    {
                        case EffectTargetType.Enemies:
                            targets = GameManager.instance.enemyCharacters;
                            break;
                        case EffectTargetType.ALL:
                            targets = GameManager.instance.allCharacters;
                            break;
                        case EffectTargetType.Our:
                            targets = GameManager.instance.playerCharacters;
                            break;
                        case EffectTargetType.Random:
                            target = GameManager.instance.randomCharacter;
                            break;
                    }
                }
            }

            //当拖拽到屏幕上方y大于1f，就可以执行
            canExecute = worldPos.y > 1f;
        }
        else//箭头&&执行目标——单个目标
        {
            if (eventData.pointerEnter == null)
            {
                canExecute = false;
                target = null;
                Debug.Log("单个目标是空");
                return;
            }

            if (eventData.pointerEnter.CompareTag("Enemy") && cardDataSO.effects[0].targetType == EffectTargetType.Target && owner is Player)
            {
                canExecute = true;
                target = eventData.pointerEnter.GetComponent<CharacterBase>();
                Debug.Log("目标是敌人&&玩家打出");
                return;
            }
            else if (eventData.pointerEnter.CompareTag("Player") && cardDataSO.effects[0].targetType == EffectTargetType.Self && owner is Player)
            {
                canExecute = true;
                target = eventData.pointerEnter.GetComponent<CharacterBase>();
                Debug.Log("目标是玩家&&玩家打出");
                return;
            }
            else if (eventData.pointerEnter.CompareTag("Player") && cardDataSO.effects[0].targetType == EffectTargetType.Target && owner is Enemy)
            {
                canExecute = true;
                target = eventData.pointerEnter.GetComponent<CharacterBase>();
                Debug.Log("目标是玩家&&敌人打出");
                return;
            }
            else if (eventData.pointerEnter.CompareTag("Enemy") && cardDataSO.effects[0].targetType == EffectTargetType.Self && owner is Enemy)
            {
                canExecute = true;
                target = eventData.pointerEnter.GetComponent<CharacterBase>();
                Debug.Log("目标是敌人&&敌人打出");
                return;
            }
            else
            {
                canExecute = false;
                target = null;
            }
        }
        ShowAllTargetEffects();
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isAvailiable || ownerIsEnemy)
        {
            return;
        }

        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }
        if (canExecute)
        {
            if(targets != null)
                ExecuteCardEffects(owner, targets);
            else if (target != null)
                ExecuteCardEffects(owner, target);

            Debug.Log("执行卡牌");
        }
        else
        {
            //卡牌回归原位
            ResetCardTransform();
            isAnimating = false;//拖拽结束后，若是回到原位卡牌仍旧可以上提
        }

        HideAllTargetEffects();
        target = null;
        targets = null;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isAnimating || !ownerIsEnemy)
        {
            // todo 如果当前是扇形布局的话，可以将 transform.position.y 修改为一个固定值，比如 3.5
            transform.position = originalPosition + Vector3.up;//这里用新变量保存，防止多次经过卡牌时，transform.position.y 不断增加
            transform.rotation = Quaternion.identity;
            GetComponent<SortingGroup>().sortingOrder = 20;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isAnimating || !ownerIsEnemy)
            ResetCardTransform();
    }

    // 显示所有可能的攻击目标的特效
    private void ShowAllTargetEffects()
    {
        if (targets != null)
        {
            foreach (var target in targets)
            {
                target.StartFlashing();
            }
        }
        if (target != null)
        {
            target.StartFlashing();
        }
    }

    // 隐藏所有可能的攻击目标的特效
    private void HideAllTargetEffects()
    {
        if (targets != null)
        {
            foreach (var target in targets)
            {
                target.StopFlashing();
            }
        }
        if (target != null)
        {
            target.StopFlashing();
        }
    }


    public void ExecuteCardEffects(CharacterBase from, List<CharacterBase> targets)
    {
        // 减少对应能量，通知回收卡牌
        

        //防止卡牌的两种效果为空
        if (cardDataSO.effects != null)//拿副本执行效果
        {
            foreach (var effect in cardDataSO.effects)
            {
                effect.Execute(from, targets);
            }
        }

        //if (cardDataSO.statusEffects != null)
        //{
        //    foreach (var statusEffect in cardDataSO.statusEffects)
        //    {
        //        //根据状态效果的类型执行不同的效果
        //        target.AddStatusEffect(statusEffect);
        //    }
        //}
        if(owner is Player)
            EventManager.Instance.TriggerEvent<object>("PlayerUseCard", this);
        else if (owner is Enemy)
            EventManager.Instance.TriggerEvent<object>("EnemyUseCard", this);
    }

    public void ExecuteCardEffects(CharacterBase from, CharacterBase target)
    {


        //防止卡牌的两种效果为空
        if (cardDataSO.effects != null)//拿副本执行效果
        {
            foreach (var effect in cardDataSO.effects)
            {
                effect.Execute(from, target);
            }
        }

        //if (cardDataSO.statusEffects != null)
        //{
        //    foreach (var statusEffect in cardDataSO.statusEffects)
        //    {
        //        //根据状态效果的类型执行不同的效果
        //        target.AddStatusEffect(statusEffect);
        //    }
        //}

        //TOOD: 减少对应能量，通知回收卡牌
        if (owner is Player)
            EventManager.Instance.TriggerEvent<object>("PlayerUseCard", this);
        else if (owner is Enemy)
            EventManager.Instance.TriggerEvent<object>("EnemyUseCard", this);
    }

    public void UpdatePositionRotation(Vector3 position, Quaternion rotation)
    {
        this.originalPosition = position;
        this.originalRotation = rotation;
        this.originalLayerOrder = GetComponent<SortingGroup>().sortingOrder;
    }

    //更新卡牌状态颜色标识玩家的能量是否够用——判断是否能使用
    public void UpdateCardState()
    {
        isAvailiable = cardDataSO.cost <= owner.currentMP;
        costText.color = isAvailiable ? Color.green : Color.red;
    }

    // 重置卡牌位置——鼠标离开时调用
    public void ResetCardTransform()
    {
        transform.SetPositionAndRotation(originalPosition, originalRotation);
        GetComponent<SortingGroup>().sortingOrder = originalLayerOrder;
    }

}

