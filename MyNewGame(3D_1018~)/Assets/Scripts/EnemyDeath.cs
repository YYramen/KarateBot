using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G���|���ꂽ�Ƃ��ɌĂяo�����v���n�u���������߂̃R���|�[�l���g
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
