using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotate : MonoBehaviour
{
    /// <summary>
    /// UI�̌�������ɃJ�����Ɍ�����悤�������邽�߂̃R���|�[�l���g
    /// </summary>
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
