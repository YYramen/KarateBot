using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵が倒されたときに呼び出されるプレハブを消すためのコンポーネント
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
