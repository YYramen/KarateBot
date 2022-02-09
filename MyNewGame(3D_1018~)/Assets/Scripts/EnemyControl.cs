using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// �G�𐧌䂷��R���|�[�l���g
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    // �ړ�����֌W
    [Tooltip("�p�j����|�C���g�̍��W�����Ă��� Vector3 �̔z��")]
    [SerializeField] Vector3[] _wayPoints = new Vector3[3];
    [Tooltip("���ڎw���|�C���g�����Ă����ϐ�")]
    int _currentPoint;
    [Tooltip("�s���p�^�[���𕪂��邽�߂̕ϐ�")]
    int _mode;
    [Tooltip("�G�̈ʒu������ϐ�")]
    [SerializeField] Transform _enemy = default;
    [SerializeField] Animator _anim = default;
    //[SerializeField] GameObject _player = default;
    [SerializeField] float _enemySight = 3f;
    [SerializeField] float _interval = 3f;
    [SerializeField] float _timer = 3f;

    // �X�e�[�^�X�֌W
    [SerializeField] StatusController _hp;
    [SerializeField] StatusController _atk;
    [SerializeField] Slider _hpSlider = default;
    [SerializeField] float _giveExp = 5f;
    float _currentHp;

    //�_���[�W�\��
    [SerializeField] GameObject _damageText = default;
    [SerializeField] GameObject _damageCanvas = default;
    [SerializeField] Transform _uiPos = default;

    //�|���ꂽ�Ƃ��ɌĂяo���v���n�u
    [SerializeField] GameObject _deathPrefab = default;
 
    NavMeshAgent _agent;
    float _firstAttack = 0f;
    float _distance = 0f;
    bool _isAttack = false;



    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        //_player = GameObject.FindGameObjectWithTag("Player");

        _hp = GetComponent<StatusController>();
        _atk = GetComponent<StatusController>();
        //_playerAtk = GetComponent<PlayerController>();
        _currentHp = _hp.Health;

    }

    // Update is called once per frame
    void Update()
    {
        //_distance = Vector3.Distance(this.transform.position, _player.transform.position);
        //if (_distance < _enemySight)
        //{
        //    _agent.destination = _player.transform.position;
        //}

        Vector3 pos = _wayPoints[_currentPoint];
        float dis = Vector3.Distance(_enemy.position, PlayerController.Instance.transform.position);

        if(dis > 5)
        {
            _mode = 0;
        }

        if(dis < 5)
        {
            _mode = 1;
        }

        switch(_mode)
        {
            case 0:
                
                if(Vector3.Distance(transform.position, pos) > 1f)
                {
                    _currentPoint += 1;
                    if(_currentPoint > _wayPoints.Length - 1)
                    {
                        _currentPoint = 0;
                    }
                }
                GetComponent<NavMeshAgent>().SetDestination(pos);
                break;

            case 1:

                _agent.destination = PlayerController.Instance.transform.position;
                break;
        }

        if(_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
            _anim.SetBool("Attack", _isAttack);
        }

        if (_currentHp <= 0)
        {
            _agent.destination = this.transform.position;
            _currentHp = 0;
            PlayerController.Instance.AddExp(_giveExp);
            Debug.Log("�G���|���ꂽ");
            Instantiate(_deathPrefab, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        //HP�֌W
        //_hpSlider.value = _currentHp / _hp.Health;
    }

    //public void ViewDamage(int _damage)
    //{
    //    _damage = _playerAtk.
    //}

    public void TakeDamage()
    {
        //�V���O���g���N���X��PlayerController�̍U���͂��Q�Ƃ��Ă���
        float damage = PlayerController.Instance.CurrentAttack;
        _currentHp -= damage;
        Debug.Log("�G���v���C���[����" + damage + "�_���[�W���󂯂�");
        //_damageText.text = damage.ToString();
        //Instantiate(_damageText, transform.position, transform.rotation);
    }

    public void ShowDamage()
    {
        var go = Instantiate<GameObject>(_damageText,_uiPos.position,Quaternion.identity,_damageCanvas.transform);
        go.GetComponent<Text>().text = PlayerController.Instance.CurrentAttack.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _agent.velocity = Vector3.zero; // �͈͓��̎��͓����Ȃ��悤�ɂ���
            _isAttack = true; // �U���t���O�� true �ɂ���
            Debug.Log($"�G��{other.name}�ƐڐG");
            this.transform.LookAt(other.transform.position); // �R���C�_�[���̓G�������悤�ɂ���

            // �U�����󂯂鏈��
            _timer += Time.deltaTime;
            if (_timer > _interval)
            {
                //_currentHp -= other.GetComponent<StatusController>().Attack;]
                TakeDamage();
                ShowDamage();
                Debug.Log($"�G�̎c��HP��{_currentHp}");
                _timer = 0f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SkillW"))
        {
            _currentHp -= PlayerController.Instance.SkillWatk; 
        }
        else if (other.CompareTag("SkillE"))
        {
            _currentHp -= PlayerController.Instance.SkillEatk;
        }
    }
}
