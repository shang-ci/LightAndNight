// UIManager.cs
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;
    
    public RectTransform statsPanel;
    public TextMeshProUGUI[] statTexts; // 按顺序分配：0-角色名，1-真挚，2-果决...

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}