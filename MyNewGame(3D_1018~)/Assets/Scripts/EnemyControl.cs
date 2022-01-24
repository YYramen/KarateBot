using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の動き、通常攻撃を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    // 移動制御関係
    [SerializeField] Animator _anim = default;
    [SerializeField] GameObject _player = default;
    [SerializeField] float _enemySight = 3f;
    [SerializeField] float _interval = 3f;

    // ステータス関係
    [SerializeField] StatusController _hp;
    [SerializeField] StatusController _atk;
    float _currentHp;

    NavMeshAgent _agent;
    float _firstAttack = 0f;
    float _distance = 0f;
    bool _isAttack = false;



    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _hp = GetComponent<StatusController>();
        _atk = GetComponent<StatusController>();
        _currentHp = _hp.Health;

    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector3.Distance(this.transform.position, _player.transform.position);
        if (_distance < _enemySight)
        {
            _agent.destination = _player.transform.position;
        }

        if(_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
            _anim.SetBool("Attack", _isAttack);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //_isAttack = true;
            this.transform.LookAt(other.transform.position);
            _firstAttack += Time.deltaTime;
            if (_firstAttack > _interval)
            {
                _isAttack = true;
                Debug.Log("敵の通常攻撃");
                _firstAttack = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{other.name}から離れた");
            _firstAttack = 0f;
            _isAttack = false;
        }
    }
}
