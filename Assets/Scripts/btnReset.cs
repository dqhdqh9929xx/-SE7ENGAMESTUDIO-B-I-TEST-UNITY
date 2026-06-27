using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnReset : MonoBehaviour
{
    /// <summary>
    /// Gắn hàm này vào sự kiện OnClick của Button trong Inspector
    /// </summary>
    public void ReloadCurrentScene()
    {
        // Lấy scene hiện tại đang active
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Load lại scene đó theo buildIndex
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
