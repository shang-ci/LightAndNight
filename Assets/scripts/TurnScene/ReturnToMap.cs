using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMap : MonoBehaviour
{
    public void OnReturnButtonClick()
    {
        // 找到并禁用 SampleScene 的 Canvas
        // Canvas sampleSceneCanvas = FindObjectOfType<Canvas>();
        // if (sampleSceneCanvas != null)
        // {
        //     sampleSceneCanvas.gameObject.SetActive(false);
        // }

        GameObject sampleCanvas = GameObject.FindGameObjectWithTag("SampleSceneCanvas");

        if (sampleCanvas != null) Destroy(sampleCanvas);
        
        SceneManager.LoadScene("Map"); // 跳转到 Map Scene
    }
}