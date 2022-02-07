using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// 敵の動き、通常攻撃を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    // 移動制御関係
    [Tooltip("徘徊するポイントの座標を入れておく Vector3 の配列")]
    [SerializeField] Vector3[] _wayPoints = new Vector3[3];
    [Tooltip("今目指すポイントを入れておく変数")]
    int _currentPoint;
    [Tooltip("行動パターンを分けるための変数")]
    int _mode;

    [Tooltip("プレイヤーの位置を入れる変数")]
    [SerializeField] Transform _player = default;
    [Tooltip("敵の位置を入れる変数")]
    [SerializeField] Transform _enemy = default;
    [SerializeField] Animator _anim = default;
    //[SerializeField] GameObject _player = default;
    [SerializeField] float _enemySight = 3f;
    [SerializeField] float _interval = 3f;
    [SerializeField] float _firstInterval = 0;

    // ステータス関係
    [SerializeField] StatusController _hp;
    [SerializeField] StatusController _atk;
    float _currentHp;

    //ダメージ表示
    [SerializeField] Text _damageText = default;

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
        float dis = Vector3.Distance(_enemy.position, _player.position);

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

                _agent.destination = _player.transform.position;
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
            _anim.SetTrigger("Death");
            Destroy(gameObject, 5);
        }
    }

    //public void ViewDamage(int _damage)
    //{
    //    _damage = _playerAtk.
    //}

    public void TakeDamage()
    {
        //シングルトンクラスのPlayerControllerの攻撃力を参照している
        float damage = PlayerController.Instance.CurrentAttack;
        _currentHp -= damage;
        _damageText.text = damage.ToString();
        Instantiate(_damageText, transform.position, transform.rotation);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isAttack = true; // 攻撃フラグを true にする
            Debug.Log($"{other.name}と接触");
            this.transform.LookAt(other.transform.position); // コライダー内の敵を向くようにする

            // 攻撃を受ける処理
            _firstInterval += Time.deltaTime;
            if (_firstInterval > _interval)
            {
                //_currentHp -= other.GetComponent<StatusController>().Attack;]
                TakeDamage();
                Debug.Log(_currentHp);
                _firstInterval = 0f;
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
