using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Esc�������ꂽ�Ƃ��ɌĂяo�����A�|�[�Y��ʂ̃R���|�[�l���g
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
