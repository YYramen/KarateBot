using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// パネルをフェードさせるコンポーネント
/// </summary>
public class FadePanel : MonoBehaviour
{
    [SerializeField] Image _fadePanel = default;

    float _alpha = 0.0f;
    float _fadeSpeed = 0.002f;
    float _fadeOutSpeed = 10f;
    
    void Start()
    {
        StartCoroutine("FadeIn");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        Color c = _fadePanel.color;
        c.a = _alpha;
        _fadePanel.color = c;
        while (true)
        {
            yield return null;
            c.a += _fadeSpeed * _fadeOutSpeed; ;
            _fadePanel.color = c;

            if (c.a >= 1)
            {
                c.a = 1f;
                _fadePanel.color = c;
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
                break;
            }
        }
    }
}
