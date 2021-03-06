using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/// <summary>
/// 敵を制御するコンポーネント
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
    [Tooltip("敵の位置を入れる変数")]
    [SerializeField] Transform _enemy = default;
    [SerializeField] Animator _anim = default;
    [SerializeField] float _enemySight = 3f;
    [SerializeField] float _interval = 3f;
    [SerializeField] float _timer = 3f;

    // ステータス関係
    [SerializeField] StatusController _hp;
    [SerializeField] StatusController _atk;
    [SerializeField] Slider _hpSlider = default;
    [SerializeField] float _giveExp = 1f;
    float _currentHp;

    //ダメージ表示
    [SerializeField] GameObject _damageText = default;
    [SerializeField] GameObject _damageCanvas = default;
    [SerializeField] Transform _uiPos = default;

    //倒されたときに呼び出す
    [SerializeField] GameObject _deathPrefab = default;
    Slider _missionSlider = default;
    float _plsMission = 0.2f;

    float _destroyDistance = 100;
    NavMeshAgent _agent;
    float _firstAttack = 0f;
    float _distance = 0f;
    bool _isAttack = false;



    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        _hp = GetComponent<StatusController>();
        _atk = GetComponent<StatusController>();
        _currentHp = _hp.Health;

        if (!_missionSlider)
        {
            _missionSlider = GameObject.Find("MissionSlider").GetComponent<Slider>();
        }
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        Vector3 pos = _wayPoints[_currentPoint];
        float dis = Vector3.Distance(_enemy.position, PlayerController.Instance.transform.position);

        //Player との距離が 100 以上になったら消し、100未満なら出現させる
        if (dis > _destroyDistance)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        if (dis > 5)
        {
            _mode = 0;
        }

        if (dis < 5)
        {
            _mode = 1;
        }

        switch (_mode)
        {
            case 0:

                if (Vector3.Distance(transform.position, pos) > 1f)
                {
                    _currentPoint += 1;
                    if (_currentPoint > _wayPoints.Length - 1)
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

        if (_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
            _anim.SetBool("Attack", _isAttack);
        }

        if (_currentHp <= 0)
        {
            _agent.destination = this.transform.position;
            _currentHp = 0;
            PlayerController.Instance.AddExp(_giveExp);
            _missionSlider.value += _plsMission;
            Debug.Log("敵が倒された");
            Instantiate(_deathPrefab, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 攻撃を受ける処理
    /// </summary>
    public void TakeDamage()
    {
        //シングルトンクラスのPlayerControllerの攻撃力を参照している
        float damage = PlayerController.Instance.CurrentAttack;
        _currentHp -= damage;
        Debug.Log("敵がプレイヤーから" + damage + "ダメージを受けた");
    }

    /// <summary>
    /// 受けたダメージをシーン上に表示する処理
    /// </summary>
    public void ShowDamage()
    {
        var go = Instantiate<GameObject>(_damageText, _uiPos.position, Quaternion.identity, _damageCanvas.transform);
        go.GetComponent<Text>().text = PlayerController.Instance.CurrentAttack.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _agent.velocity = Vector3.zero; // 範囲内の時は動かないようにする
            _isAttack = true; // 攻撃フラグを true にする
            Debug.Log($"敵が{other.name}と接触");
            this.transform.LookAt(other.transform.position); // コライダー内の敵を向くようにする

            // 攻撃を受ける処理
            _timer += Time.deltaTime;
            if (_timer > _interval)
            {
                TakeDamage();
                ShowDamage();
                Debug.Log($"敵の残りHPは{_currentHp}");
                _timer = 0f;
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
