using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �^�C�g���̕�����h�炷�R���|�[�l���g
/// </summary>
public class ShakeText : MonoBehaviour
{
    public Transform _textPos;      //�e�L�X�g��transform

    public float shakePower;            // �h�炷����

    Vector3 moneyTextInitPos;           // �J�n���̈ʒu

    private void Start ()
    {
        // �J�n���̈ʒu���擾
        moneyTextInitPos = _textPos.position;
    }


    private void Update ()
    {
        // �����_���ɗh�炷
        _textPos.position = moneyTextInitPos + Random.insideUnitSphere * shakePower;
    }
}
