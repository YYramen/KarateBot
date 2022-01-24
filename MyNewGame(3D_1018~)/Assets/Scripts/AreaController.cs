using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの通常攻撃の範囲を可視化するためのコンポーネント
/// </summary>

public class AreaController : MonoBehaviour
{
    [Tooltip("PlayerMarkerを指定")]
    [SerializeField] GameObject _marker = default;

    SpriteRenderer _renderer = default;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = Color.white;
    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _renderer.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _renderer.color = Color.white;
        }
    }
}
