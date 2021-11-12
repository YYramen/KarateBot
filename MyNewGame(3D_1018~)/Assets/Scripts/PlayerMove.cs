using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] float _jumpPower = 3;
    Rigidbody _rb = default;
    Animator _anim = default;
    bool _isGrounded = true;
    bool _isAttack1 = false;
    bool _isAttack2 = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 入力を受け付ける
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 入力された方向を「カメラを基準とした XZ 平面上のベクトル」に変換する
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        // キャラクターを「入力された方向」に向ける
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }

        // Y 軸方向の速度を保ちながら、速度ベクトルを求めてセットする
        Vector3 velocity = dir.normalized * _moveSpeed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;

        // ジャンプ処理
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }

        // 攻撃
        if (Input.GetButtonDown("Fire1"))
        {
            Attack1();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Attack2();
        }
    }

    void Attack1()
    {
        if (_isAttack1 == false)
        {
            _anim.SetBool("Attack1", true);
        }
    }

    void Attack2()
    {
        if(_isAttack2 == false)
        {
            _anim.SetBool("Attack2", true);
        }
    }

    private void LateUpdate()
    {
        if (_anim)
        {
            _anim.SetBool("Attack2", _isAttack2);
            _anim.SetBool("Attack1", _isAttack1);
            _anim.SetBool("IsGrounded", _isGrounded);
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }
}
