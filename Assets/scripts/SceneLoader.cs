using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 目标移动距离（像素）
    public float triggerDistance;
    // 目标场景名称（需与 Build Settings 中的名称一致）
    public string targetSceneName = "SampleScene";
    public float pixelDistance; // 当前移动距离（像素）

    private Vector2 initialPosition; // 初始位置（世界坐标）
    public bool hasTriggered = false; // 防止重复触发

    private void Start()
    {
        // 记录初始位置
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (hasTriggered) return;

        // 计算当前移动距离（世界坐标差值）
        Vector2 currentPosition = transform.position;
        float movedDistance = Vector2.Distance(initialPosition, currentPosition);

        // 转换为像素距离（假设 1 Unity单位 = 100像素，根据项目实际PPU调整）
        pixelDistance = movedDistance * 100f;

        // 检测是否达到触发距离
        if (pixelDistance >= triggerDistance)
        {
            hasTriggered = true;
            SceneManager.LoadScene(targetSceneName);

            //找到之前禁用 SampleScene 的 Canvas
            // Canvas sampleSceneCanvas = FindObjectOfType<Canvas>();
            // if (sampleSceneCanvas != null)
            // {
            //     sampleSceneCanvas.gameObject.SetActive(true);
            // }

            initialPosition = transform.position; // 重置初始位置
        }
    }
}