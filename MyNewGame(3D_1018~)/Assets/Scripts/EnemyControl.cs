using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̓����A�ʏ�U���𐧌䂷��R���|�[�l���g
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    [SerializeField] Animator _anim = default;
    [SerializeField] GameObject _player = default;
    [SerializeField] float _enemySight = 3f;
    [SerializeField] float _interval = 5f;
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
                Debug.Log("�G�̒ʏ�U��");
                _firstAttack = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{other.name}���痣�ꂽ");
            _firstAttack = 0f;
            _isAttack = false;
        }
    }
}
