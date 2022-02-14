using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトルの文字を揺らすコンポーネント
/// </summary>
public class ShakeText : MonoBehaviour
{
    public Transform _textPos;      //テキストのtransform

    public float shakePower;            // 揺らす強さ

    Vector3 moneyTextInitPos;           // 開始時の位置

    private void Start ()
    {
        // 開始時の位置を取得
        moneyTextInitPos = _textPos.position;
    }


    private void Update ()
    {
        // ランダムに揺らす
        _textPos.position = moneyTextInitPos + Random.insideUnitSphere * shakePower;
    }
}
