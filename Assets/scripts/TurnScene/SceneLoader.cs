using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    // 在Inspector中配置可跳转的场景列表
    public List<string> targetScenes = new List<string>
    {
        "SampleScene",
        "SampleScene1",
        "SampleScene2"
    };

    [Header("触发设置")]
    // 目标移动距离（像素）
    public float triggerDistance;
    // 目标场景名称（需与 Build Settings 中的名称一致）
    public float pixelDistance; // 当前移动距离（像素）

    private Vector2 initialPosition; // 初始位置（世界坐标）
    public bool hasTriggered = false; // 防止重复触发

      private void Start()
    {
        InitializePosition();
    }

    private void Update()
    {
        if (hasTriggered || targetScenes.Count == 0) return;

        UpdateDistance();
        CheckTriggerCondition();
    }

    private void InitializePosition()
    {
        initialPosition = transform.position;
    }

    private void UpdateDistance()
    {
        Vector2 currentPosition = transform.position;
        float movedDistance = Vector2.Distance(initialPosition, currentPosition);
        pixelDistance = movedDistance * 100f; // 假设1单位=100像素
    }

    // 检测触发条件
    private void CheckTriggerCondition()
    {
        if (pixelDistance >= triggerDistance)
        {
            hasTriggered = true;
            LoadRandomScene();
        }
    }

    // 随机加载场景的核心逻辑
    private void LoadRandomScene()
    {
        // 生成随机索引（排除当前场景）
        int randomIndex = GetRandomSceneIndex();
        
        // 加载场景
        SceneManager.LoadScene(targetScenes[randomIndex]);
        
        // 重置初始位置（如果需要保持连续触发可取消注释）
        // initialPosition = transform.position;
        // hasTriggered = false;
    }

    // 获取不重复的随机场景索引
    private int GetRandomSceneIndex()
    {
        // 获取当前场景名称
        string currentScene = SceneManager.GetActiveScene().name;

        // 创建有效场景列表（排除当前场景）
        List<string> validScenes = new List<string>();
        foreach (string scene in targetScenes)
        {
            if (scene != currentScene)
            {
                validScenes.Add(scene);
            }
        }

        // 如果没有可用场景则随机选择（包含当前场景）
        if (validScenes.Count == 0)
        {
            validScenes = new List<string>(targetScenes);
        }

        // 生成随机索引
        return Random.Range(0, validScenes.Count);
    }

    // 重置触发器（可在其他脚本中调用）
    public void ResetTrigger()
    {
        hasTriggered = false;
        InitializePosition();
    }
}
