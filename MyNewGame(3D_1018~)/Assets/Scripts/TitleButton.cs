using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ボタンに使うスクリプト。主にシーンの移動
/// </summary>

public class TitleButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		Application.OpenURL("http://www.google.co.jp");
#else
		Application.Quit();
#endif
    }

    public void GotoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
