using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [Header("导航按钮")]
    [SerializeField] public Button cardsButton;
    [SerializeField] public Button skillsButton;
    [SerializeField] public Button inventoryButton;

    [Header("内容面板")]
    [SerializeField] public GameObject cardsPanel;
    [SerializeField] public GameObject skillsPanel;
    [SerializeField] public GameObject inventoryPanel;
    public GameObject detailPanel;

    private void Start()
    {
        // 绑定按钮点击事件
        // cardsButton.onClick.AddListener(() => ShowPanel(cardsPanel));
        // skillsButton.onClick.AddListener(() => ShowPanel(skillsPanel));
        // inventoryButton.onClick.AddListener(() => ShowPanel(inventoryPanel));
        cardsButton.onClick.AddListener(OnCardsButtonClick);
        skillsButton.onClick.AddListener(OnSkillsButtonClick);
        inventoryButton.onClick.AddListener(OnInventoryButtonClick);

        // 默认显示属性面板
        ShowPanel(cardsPanel);

        Debug.Assert(cardsButton != null, "Cards按钮未赋值");
        Debug.Assert(cardsPanel != null, "Cards面板未赋值");
        Debug.Assert(skillsButton != null, "Skills按钮未赋值");
        Debug.Assert(skillsPanel != null, "Skills面板未赋值");
        Debug.Assert(inventoryButton != null, "Inventory按钮未赋值");
        Debug.Assert(inventoryPanel != null, "Inventory面板未赋值");
    }

    private void OnCardsButtonClick()
    {
        //Debug.Log("Cards Button Clicked");
        ShowPanel(cardsPanel);
    }

    private void OnSkillsButtonClick()
    {
        //Debug.Log("Skills Button Clicked");
        ShowPanel(skillsPanel);
    }

    private void OnInventoryButtonClick()
    {
        //Debug.Log("Inventory Button Clicked");
        ShowPanel(inventoryPanel);
    }

    public void ShowPanel(GameObject targetPanel)
    {
        // 隐藏所有面板
        cardsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        inventoryPanel.SetActive(false);

        // 显示目标面板
        targetPanel.SetActive(true);

        //Debug.Log($"{targetPanel.name} 状态: {targetPanel.activeSelf}"); // 应该输出 True
    }

    public void ClickOnCloseButton()
    {
        detailPanel.SetActive(false);
    }

}