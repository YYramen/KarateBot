using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�^�X�̏������s���R���|�[�l���g
/// </summary>

public class StatusController : MonoBehaviour
{
    // �X�e�[�^�X�̏����l�A���̐����͊�{�Q�[�����͕ς��Ȃ�
    [SerializeField] float _firstHealth = 200f; 
    [SerializeField] float _firstAttack = 10f;
    [SerializeField] float _firstLevel = 1f;

    // ���ۂɃQ�[�����ɉς���l
    float _health; 
    float _attack;
    float _level;
    
    //�ǂݎ��\�ɂ���
    public float Health => _health;

    public float Attack => _attack;

    public float Level => _level;


    // �Q�[���J�n���Ɏ��ۂɓ������l�ɏ����l����
    void Awake()
    {
        _health = _firstHealth;
        _attack = _firstAttack;
        _level = _firstLevel;
    }

    // �l���ς����鎞�ɌĂяo���֐�
    public void HealthFlucture(float value) 
    {
        _health += value;
    }

    public void AttackFlucture(float value)
    {
        _attack += value;
    }

    public void LevelFlucture(float value)
    {
        _level += value;
    }
}
