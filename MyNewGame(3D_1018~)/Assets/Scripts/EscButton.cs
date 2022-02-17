using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Escが押されたときに呼び出される、ポーズ画面のコンポーネント
/// </summary>

public class EscButton : MonoBehaviour
{
    [SerializeField] GameObject _escUI = default;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _escUI.SetActive(!_escUI.activeSelf);

            if (_escUI.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
