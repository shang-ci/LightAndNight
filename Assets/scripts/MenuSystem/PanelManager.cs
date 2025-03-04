using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [Header("导航按钮")]
    [SerializeField] private Button attributesButton;
    [SerializeField] private Button skillsButton;
    [SerializeField] private Button inventoryButton;

    [Header("内容面板")]
    [SerializeField] private GameObject attributesPanel;
    [SerializeField] private GameObject skillsPanel;
    [SerializeField] private GameObject inventoryPanel;

    private void Start()
    {
        // 绑定按钮点击事件
        attributesButton.onClick.AddListener(() => ShowPanel(attributesPanel));
        skillsButton.onClick.AddListener(() => ShowPanel(skillsPanel));
        inventoryButton.onClick.AddListener(() => ShowPanel(inventoryPanel));

        // 默认显示属性面板
        ShowPanel(attributesPanel);
    }

    private void ShowPanel(GameObject targetPanel)
    {
        // 隐藏所有面板
        attributesPanel.SetActive(false);
        skillsPanel.SetActive(false);
        inventoryPanel.SetActive(false);

        // 显示目标面板
        targetPanel.SetActive(true);
    }
}