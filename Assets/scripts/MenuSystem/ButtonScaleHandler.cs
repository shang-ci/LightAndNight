using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;

    [SerializeField]public float scale;

    public void Start()
    {
        // 记录按钮的原始大小
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 鼠标移至按钮上时，将按钮放大1.5倍
        transform.localScale = originalScale * scale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 鼠标移出按钮时，将按钮恢复到原始大小
        transform.localScale = originalScale;
    }
}