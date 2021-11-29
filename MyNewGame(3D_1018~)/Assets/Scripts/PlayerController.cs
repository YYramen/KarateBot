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
    [Tooltip("初期位置")]
    [SerializeField] Vector3 _firstPosition;
    [Tooltip("移動先の位置情報")]
    [SerializeField] Transform _target = default;
    [Tooltip("移動先の座標を保存する変数")]
    Vector3 _changedTargetPosition;
    [Tooltip("プレイヤーのアニメーションを指定")]
    [SerializeField] Animator _anim = default;

    bool[] skills = new bool[4];
    bool _isAttack = false;
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
            _anim.SetBool("Attack", _isAttack);
        }

        // Escape キーを押したら初期位置に戻る
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escapeが押された、初期位置に戻る");
            this.transform.position = _firstPosition;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Skill(0);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            this.transform.LookAt(other.transform.position);
            Debug.Log($"{other.name}と接触");
            _isAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"{other.name}から離れた");
            _isAttack = false;
        }
    }
}
