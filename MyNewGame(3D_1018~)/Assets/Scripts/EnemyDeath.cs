using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G���|���ꂽ�Ƃ��ɌĂяo�����R���|�[�l���g�i���Ԃ��o������ Destroy ���邾���j
/// </summary>
public class EnemyDeath : MonoBehaviour
{
    float _time = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);   
    }
}
