using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarControl : MonoBehaviour
{
    public CharacterBaseNew currentCharacter;

    [Header("Elements")]

    public Transform healthBarTransform;

    private UIDocument healthBarDocument;

    private ProgressBar healthBar;

    private VisualElement defenceElement;

    private Label defenceAmountLabel;

    private VisualElement buffElements;

    private Label buffRound;

    // [Header("buff sprite")]
    // public Sprite buffSprite;

    // public Sprite debuffSprite;

    private Boss boss;

    private VisualElement intentSprite;

    private Label intentAmount;

    private void Awake()
    {
        currentCharacter = GetComponent<CharacterBaseNew>();
        if (currentCharacter == null)
        {
            Debug.LogError("currentCharacter is null. Please ensure it is assigned properly.");
        }

        boss = GetComponent<Boss>();
        if (boss == null)
        {
            Debug.LogError("enemy is null. Please ensure it is assigned properly.");
        }
    }

    // private void Awake()
    // {
    //     currentCharacter = GetComponent<CharacterBase>();

    //     enemy = GetComponent<Enemy>(); 
    // }

    private void OnEnable()
    {
        InitHealthBar();
    }

    [ContextMenu("Get UI Position")]
    public void InitHealthBar()
    {
        healthBarDocument = GetComponent<UIDocument>();
        if (healthBarDocument == null)
        {
            Debug.LogError("healthBarDocument is null. Please ensure it is assigned properly.");
            return;
        }

        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        if (healthBar == null)
        {
            Debug.LogError("healthBar is null. Please ensure it is assigned properly.");
            return;
        }

        healthBar.highValue = currentCharacter.MaxHP;
        MoveToWorldPosition(healthBar, healthBarTransform.position, Vector2.zero);

        defenceElement = healthBar.Q<VisualElement>("Defence");
        defenceAmountLabel = defenceElement.Q<Label>("DefenceAmount");
        defenceElement.style.display = DisplayStyle.None;

        // buffElements = healthBar.Q<VisualElement>("Buff");
        // buffRound = buffElements.Q<Label>("BuffRound");
        // buffElements.style.display = DisplayStyle.None;

        // intentSprite = healthBar.Q<VisualElement>("Intent");
        // intentAmount = healthBar.Q<Label>("IntendAmount");
        // intentSprite.style.display = DisplayStyle.None;
    }
    // private void OnEnable()
    // {
    //     InitHealthBar();
    // }

    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 size)
    {
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size, Camera.main);

        element.transform.position = rect.position;
    }


    private void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (currentCharacter.isDead)
        {
            if (healthBar != null)
            {
                healthBar.style.display = DisplayStyle.None;
            }
            return;
        }

        if (healthBar != null)
        {
            healthBar.title = $"{currentCharacter.CurrentHP} / {currentCharacter.MaxHP}";
            healthBar.value = currentCharacter.CurrentHP;

            healthBar.RemoveFromClassList("highHealth");
            healthBar.RemoveFromClassList("mediumHealth");
            healthBar.RemoveFromClassList("lowHealth");

            var percentage = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;

            if (percentage < 0.3f)
            {
                healthBar.AddToClassList("lowHealth");
            }
            else if (percentage < 0.6f)
            {
                healthBar.AddToClassList("mediumHealth");
            }
            else
            {
                healthBar.AddToClassList("highHealth");
            }

            // 防御属性更新
            if (defenceElement != null && defenceAmountLabel != null)
            {
                defenceElement.style.display = currentCharacter.defence.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
                defenceAmountLabel.text = currentCharacter.defence.currentValue.ToString();
            }
            else
            {
                Debug.LogError("defenceElement or defenceAmountLabel is null. Please ensure they are initialized properly.");
            }

            // buff回合更新
            if (buffElements != null && buffRound != null)
            {
                buffElements.style.display = currentCharacter.buffRound.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
                buffRound.text = currentCharacter.buffRound.currentValue.ToString();
            }
            else
            {
                Debug.LogError("buffElements or buffRound is null. Please ensure they are initialized properly.");
            }

            // buffElements.style.backgroundImage = currentCharacter.baseStrength > 1 ? new StyleBackground(buffSprite) : new StyleBackground(debuffSprite);
        }
        else
        {
            Debug.LogError("healthBar is null. Please ensure it is initialized properly.");
        }
    }
    // public void UpdateHealthBar()
    // {
    //     if (currentCharacter.isDead)
    //     {
    //         healthBar.style.display = DisplayStyle.None;
    //         return;
    //     }

    //     if (healthBar != null)
    //     {
    //         healthBar.title = $"{currentCharacter.CurrentHP} / {currentCharacter.MaxHP}";
    //         healthBar.value = currentCharacter.CurrentHP;

    //         healthBar.RemoveFromClassList("highHealth");
    //         healthBar.RemoveFromClassList("mediumHealth");
    //         healthBar.RemoveFromClassList("lowHealth");

    //         var percentage = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;

    //         if (percentage < 0.3f)
    //         {
    //             healthBar.AddToClassList("lowHealth");
    //         }
    //         else if (percentage < 0.6f)
    //         {
    //             healthBar.AddToClassList("mediumHealth");
    //         }
    //         else
    //         {
    //             healthBar.AddToClassList("highHealth");
    //         }

    //         //防御属性更新
    //         defenceElement.style.display = currentCharacter.defence.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
    //         defenceAmountLabel.text = currentCharacter.defence.currentValue.ToString();

    //         //buff回合更新
    //         buffElements.style.display = currentCharacter.buffRound.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;
    //         buffRound.text = currentCharacter.buffRound.currentValue.ToString();

    //         //buffElements.style.backgroundImage = currentCharacter.baseStrength > 1 ? new StyleBackground(buffSprite) : new StyleBackground(debuffSprite);
    //     }
    //     else
    //     {
    //         Debug.LogError("healthBar is null. Please ensure it is initialized properly.");
    //     }
    // }
    // public void UpdateHealthBar()
    // {
    //     if (currentCharacter.isDead)
    //     {
    //         healthBar.style.display = DisplayStyle.None;

    //         return;
    //     }

    //     if (healthBar != null)
    //     {
    //         healthBar.title = $"{currentCharacter.CurrentHP} / {currentCharacter.MaxHP}";

    //         healthBar.value = currentCharacter.CurrentHP;

    //         healthBar.RemoveFromClassList("highHealth");

    //         healthBar.RemoveFromClassList("mediumHealth");

    //         healthBar.RemoveFromClassList("lowHealth");

    //         var percentage = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;

    //         if (percentage < 0.3f)
    //         {
    //             healthBar.AddToClassList("lowHealth");
    //         }
    //         else if (percentage < 0.6f)
    //         {
    //             healthBar.AddToClassList("mediumHealth");
    //         }
    //         else
    //         {
    //             healthBar.AddToClassList("highHealth");
    //         }

    //         //防御属性更新
    //         defenceElement.style.display = currentCharacter.defence.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;

    //         defenceAmountLabel.text = currentCharacter.defence.currentValue.ToString();

    //         //buff回合更新
    //         buffElements.style.display = currentCharacter.buffRound.currentValue > 0 ? DisplayStyle.Flex : DisplayStyle.None;

    //         buffRound.text = currentCharacter.buffRound.currentValue.ToString();

    //         buffElements.style.backgroundImage = currentCharacter.baseStrength > 1 ? new StyleBackground(buffSprite) : new StyleBackground(debuffSprite);
    //     }
    // }

    //在玩家回合开始时
    public void SetIntentElement()
    {
        intentSprite.style.display = DisplayStyle.Flex;

        intentSprite.style.backgroundImage = new StyleBackground(boss.currentAction.intentSprite);

        //判断是否是攻击
        var value = boss.currentAction.effect.value;

        // if (enemy.currentAction.effect.GetType() == typeof(DamageEffect))
        // {
        //     value = (int)math.round(enemy.currentAction.effect.value * enemy.baseStrength);
        // }
        intentAmount.text = value.ToString();
    }

    //敌人回合结束后
    public void HideIntentElement()
    {
        intentSprite.style.display = DisplayStyle.None;
    }
}
