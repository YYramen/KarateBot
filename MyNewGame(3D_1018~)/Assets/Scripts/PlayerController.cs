using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// プレイヤーを移動させるためのコンポーネント
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("移動先の位置情報")]
    [SerializeField] Transform _target = default;
    [Tooltip("移動先の座標を保存する変数")]
    Vector3 _changedTargetPosition;
    [Tooltip("プレイヤーのアニメーションを指定")]
    [SerializeField] Animator _anim = default;
    NavMeshAgent _agent = default;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _changedTargetPosition = _target.position;
    }

    void Update()
    {
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
        }
    }
}
