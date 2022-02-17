using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ボタンに使うスクリプト。主にシーンの移動
/// </summary>

public class TitleButton : MonoBehaviour
{
    [SerializeField] AudioSource _startButtonAudio = default;
    [SerializeField] Image _fadePanel = default;

    float _alpha = 0.0f;
    float _fadeSpeed = 0.002f;
    public void StartGame()
    {
        StartCoroutine("FadeOutToGame");
        //SceneManager.LoadScene("GameScene");
    }

    public void SetTime()
    {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator FadeOutToGame()
    {
        Color c = _fadePanel.color;
        c.a = _alpha;
        _fadePanel.color = c;
        while (true)
        {
            yield return null;
            c.a += _fadeSpeed;
            _fadePanel.color = c;

            if (c.a >= 1)
            {
                c.a = 1f;
                _fadePanel.color = c;
                SceneManager.LoadScene("GameScene");
                break;
            }
        }
    }

    IEnumerator FadeOutToTitle()
    {
        Color c = _fadePanel.color;
        c.a = _alpha;
        _fadePanel.color = c;
        while (true)
        {
            yield return null;
            c.a += _fadeSpeed;
            _fadePanel.color = c;

            if (c.a >= 1)
            {
                c.a = 1f;
                _fadePanel.color = c;
                SceneManager.LoadScene("TitleScene");
                break;
            }
        }
    }


    IEnumerator FadeIn()
    {
        Color c = _fadePanel.color;
        c.a = 1;
        _fadePanel.color = c;
        while (true)
        {
            yield return null;
            c.a -= _fadeSpeed;
            _fadePanel.color = c;

            if (c.a <= 0)
            {
                c.a = 0f;
                _fadePanel.color = c;
                SceneManager.LoadScene("TitleScene");
                break;
            }
        }
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
        //SceneManager.LoadScene("TitleScene");
        StartCoroutine("FadeOutToTitle");
    }

    public void StartAudio()
    {
        _startButtonAudio.Play();
    }
}
