using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// プレイヤーを制御するコンポーネント
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : Singleton<PlayerController>
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
    [SerializeField] Slider _slider1_QSkill = default;
    [SerializeField] Slider _slider2_WSkill = default;
    [SerializeField] Slider _slider3_ESkill = default;
    float _skillTimer1 = 0f;
    float _skillTimer2 = 0f;
    float _skillTimer3 = 0f;
    [SerializeField] float _skillInterval1 = 3f;
    [SerializeField] float _skillInterval2 = 3f;
    [SerializeField] float _skillInterval3 = 3f;
    [SerializeField] Collider _skillECollider = default;

    // HP・レベル関係
    [SerializeField] Slider _hpSlider = default;
    [SerializeField] Slider _expSlider = default;
    [SerializeField] StatusController _hp;
    [SerializeField] StatusController _exp;
    [SerializeField] StatusController _atk;
    [SerializeField] Text _lvText = default;
    [SerializeField] Text _hpText = default;
    int _level = 0;
    float _currentHp;
    float _currentExp;
    float _currentAtk;
    float _skillWatk = 15f;
    float _skillEatk = 30f;
    public float CurrentAttack => _currentAtk;
    public float CurrentExp => _currentExp;
    public float SkillWatk => _skillWatk;
    public float SkillEatk => _skillEatk;

    //レベルアップするとき
    [SerializeField] GameObject _levelUpText = default;
    [SerializeField] Transform _uiPos = default;
    [SerializeField] GameObject _canvas = default;
    float _upHP = 50f;
    float _upAtk = 5f;

    // 攻撃しているかどうか
    bool _isAttack = false;

    //倒されるとき
    [SerializeField] GameObject _death = default;

    NavMeshAgent _agent = default;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _changedTargetPosition = _target.position;
        _hp = GetComponent<StatusController>();
        _exp = GetComponent<StatusController>();
        _atk = GetComponent<StatusController>();

        //ここで値を保存しておく
        _currentHp = _hp.Health;
        _currentExp = _exp.Level;
        _currentAtk = _atk.Attack;
    }

    void Update()
    {
        // プレイヤーの基本動作(移動、通常攻撃)
        // m_target が移動したら Navmesh Agent を使って移動させる
        if (Vector3.Distance(_changedTargetPosition, _target.position) > Mathf.Epsilon) // _target が移動したら
        {
            _changedTargetPosition = _target.position; // 移動先の座標を保存する
            _agent.SetDestination(_changedTargetPosition); // Navmesh Agent に目的地をセットする（Vector3 で座標を設定していることに注意。Transform でも GameObject でもなく、Vector3 で目的地を指定する）
        }

        // _animator がアサインされていたら Animator Controller にパラメーターを設定する
        if (_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
            _anim.SetBool("Jump", _agent.isOnOffMeshLink);
        }

        // Escape キーを押したら初期位置に戻る(多分これそのうち消す)
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escapeが押された、初期位置に戻る");
            this.transform.position = _firstPosition;
        }


        // スキル関係
        // スキルのクールダウンゲージの処理

        if (_slider1_QSkill.value != 1)
        {
            _slider1_QSkill.value += 1 / _skillInterval1 * Time.deltaTime;
        }

        if (_slider2_WSkill.value != 1)
        {
            _slider2_WSkill.value += 1 / _skillInterval2 * Time.deltaTime;
        }

        if (_slider3_ESkill.value != 1)
        {
            _slider3_ESkill.value += 1 / _skillInterval3 * Time.deltaTime;
        }

        // クールダウンが終わっていてボタンが押されたらスキル発動
        if (Input.GetButtonDown("QSkill"))
        {
            if (_slider1_QSkill.value >= 1)
            {
                Skill(0);
                Debug.Log("Qスキルを発動");
                _slider1_QSkill.value = 0;
            }
        }

        if (Input.GetButtonDown("WSkill"))
        {
            if (_slider2_WSkill.value >= 1)
            {
                Skill(1);
                Debug.Log("Wスキルを発動");
                _slider2_WSkill.value = 0;
            }
        }

        if (Input.GetButtonDown("ESkill"))
        {
            if (_slider3_ESkill.value >= 1)
            {
                Skill(2);
                Debug.Log("Eスキルを発動");
                _slider3_ESkill.value = 0;
            }
        }


        //HP・レベル関係
        _hpSlider.value = _currentHp / _hp.Health;
        _expSlider.value = _currentExp / _exp.Level;
        _lvText.text = _level.ToString();
        _hpText.text = _currentHp.ToString();

        if (_expSlider.value == 1)
        {
            LevelUp();
            _expSlider.value = 0;
            _currentExp = 0;
            _level++;
        }

        if(_currentHp < 0)
        {
            _currentHp = 0;
            Debug.Log("プレイヤーが倒された");
            Instantiate(_death, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void AddExp(float exp)
    {
        _currentExp += exp;
        Debug.Log($"経験値が{exp}増えた");
    }

    void LevelUp() //レベルアップした時に呼ばれる関数
    {
        Instantiate(_levelUpText, _uiPos.position, Quaternion.identity, _canvas.transform);
        _currentHp += _upHP;
        Debug.Log($"HPが{_upHP}回復した,現在の残りHPは{_currentHp}");
        _currentAtk += _upAtk;
        Debug.Log($"攻撃力が{_upAtk}増加した");
    }

    void Skill(int skill) // アニメーショントリガー
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
            _anim.SetTrigger("Attack"); // 攻撃フラグを true にする
            Debug.Log($"プレイヤーが{other.name}と接触");
            this.transform.LookAt(other.transform.position); // コライダー内の敵を向くようにする
    
            // 攻撃を受ける処理
            _firstInterval += Time.deltaTime;
            if (_firstInterval > _interval)
            {
                Debug.Log($"プレイヤーが敵から{other.GetComponent<StatusController>().Attack}ダメージ受けた");
                _currentHp -= other.GetComponent<StatusController>().Attack;
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
        }
    }

    public void AddAttack(float addAtk)
    {
        _currentAtk += addAtk;
    }
}
