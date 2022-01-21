﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// プレイヤーを移動させるためのコンポーネント
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    // プレイヤーの基本動作
    [Tooltip("初期位置")]
    [SerializeField] Vector3 _firstPosition;
    [Tooltip("移動先の位置情報")]
    [SerializeField] Transform _target = default;
    [Tooltip("移動先の座標を保存する変数")]
    Vector3 _changedTargetPosition;
    [Tooltip("プレイヤーのアニメーションを指定")]
    [SerializeField] Animator _anim = default;
    [Tooltip("通常攻撃のインターバル")]
    [SerializeField] float _interval = 3f;
    [Tooltip("インターバルの初期値")]
    float _firstInterval = 0f;

    // スキル関連
    bool[] skills = new bool[4];
    [SerializeField] float _skillTimer1 = 0f;
    [SerializeField] float _skillTimer2 = 0f;
    [SerializeField] float _skillTimer3 = 0f;
    [SerializeField] float _skillInterval = 3f;

    // 攻撃しているかどうか
    bool _isAttack = false;

    NavMeshAgent _agent = default;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _changedTargetPosition = _target.position;
    }

    void Update()
    {
        _skillTimer1 += Time.deltaTime;
        _skillTimer2 += Time.deltaTime;
        _skillTimer3 += Time.deltaTime;

        // プレイヤーの基本動作(移動、通常攻撃)
        // m_target が移動したら Navmesh Agent を使って移動させる
        if (Vector3.Distance(_changedTargetPosition, _target.position) > Mathf.Epsilon) // _target が移動したら
        {
            _changedTargetPosition = _target.position; // 移動先の座標を保存する
            _agent.SetDestination(_changedTargetPosition); // Navmesh Agent に目的地をセットする（Vector3 で座標を設定していることに注意。Transform でも GameObject でもなく、Vector3 で目的地を指定する）
        }

        // m_animator がアサインされていたら Animator Controller にパラメーターを設定する
        if (_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
            _anim.SetBool("Jump", _agent.isOnOffMeshLink);
            _anim.SetBool("Attack", _isAttack);
        }

        // Escape キーを押したら初期位置に戻る(多分これそのうち消す)
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escapeが押された、初期位置に戻る");
            this.transform.position = _firstPosition;
        }

        // スキル関係
        if (Input.GetButtonDown("QSkill"))
        {
            Debug.Log("Qが押された");
            if (_skillInterval < _skillTimer1)
            {
                Skill(0);
                Debug.Log("Qスキルを発動");
                _skillTimer1 = 0f;
            }
        }

        if (Input.GetButtonDown("WSkill"))
        {
            Debug.Log("Wが押された");
            if (_skillInterval < _skillTimer2)
            {
                Skill(1);
                Debug.Log("Wスキルを発動");
                _skillTimer2 = 0f;
            }
        }

        if (Input.GetButtonDown("ESkill"))
        {
            Debug.Log("Eが押された");
            if (_skillInterval < _skillTimer3)
            {
                Skill(2);
                Debug.Log("Eスキルを発動");
                _skillTimer3 = 0f;
            }
        }
    }

    void Skill(int skill)
    {
        switch (skill)
        {
            case 0:
                _anim.SetTrigger("Skill1");
                break;
            case 1:
                _anim.SetTrigger("Skill2");
                break;
            case 2:
                _anim.SetTrigger("Skill3");
                break;
            case 3:
                _anim.SetTrigger("Skill4");
                break;
        }
    }

    private void OnTriggerStay(Collider other) // プレイヤーの通常攻撃の範囲内に入ったら
    {
        if (other.CompareTag("Enemy"))
        {
            _isAttack = true;
            Debug.Log($"{other.name}と接触");
            this.transform.LookAt(other.transform.position);
            _firstInterval += Time.deltaTime;
            if(_firstInterval > _interval)
            {
                Debug.Log("通常攻撃");
                _firstInterval = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other) // プレイヤーの通常攻撃の範囲から出たら
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"{other.name}から離れた");
            _firstInterval = 0f;
            _isAttack = false;
        }
    }
}
