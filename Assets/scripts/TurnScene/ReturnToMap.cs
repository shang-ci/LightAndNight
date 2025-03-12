using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMap : MonoBehaviour
{
    public void OnReturnButtonClick()
    {
        SceneManager.LoadScene("Map"); // 跳转到 Map Scene
    }
}