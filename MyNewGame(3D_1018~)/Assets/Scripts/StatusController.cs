using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステータスの処理を行うコンポーネント
/// </summary>

public class StatusController : MonoBehaviour
{
    // ステータスの初期値、この数字は基本ゲーム中は変わらない
    [SerializeField] float _firstHealth = 200f; 
    [SerializeField] float _firstAttack = 10f;
    [SerializeField] float _firstLevel = 1f;

    // 実際にゲーム中に可変する値
    float _health; 
    float _attack;
    float _level;
    
    //読み取り可能にする
    public float Health => _health;

    public float Attack => _attack;

    public float Level => _level;


    // ゲーム開始時に実際に動かす値に初期値を代入
    void Awake()
    {
        _health = _firstHealth;
        _attack = _firstAttack;
        _level = _firstLevel;
    }

    // 値を可変させる時に呼び出す関数
    public void HealthFlucture(float value) 
    {
        _health += value;
    }

    public void AttackFlucture(float value)
    {
        _attack += value;
    }

    public void LevelFlucture(float value)
    {
        _level += value;
    }
}
