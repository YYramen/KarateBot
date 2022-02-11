using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵が倒されたときに呼び出されるコンポーネント（時間が経ったら Destroy するだけ）
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    float _time = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);   
    }
}
