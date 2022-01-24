using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G���X�|�[�������邽�߂̃R���|�[�l���g
/// </summary>

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab = default; // �X�|�[��������G�̃v���n�u
    [SerializeField] Transform[] _spawnPoint = default; // �X�|�[��������ꏊ
    [SerializeField] float _spawnInterval = 3f; // �X�|�[��������Ԋu
    List<GameObject> _enemies = new List<GameObject>(); // �������𐧌����邽�ߓG���i�[���郊�X�g
    float _timer = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag =="Player")
        {
            _timer += Time.deltaTime;
            if (_spawnInterval < _timer && _enemies.Count<_spawnPoint.Length) //���Ԑ������o��,�X�|�[������łȂ��Ƃ�
            {
                int index = Random.Range(0, _spawnPoint.Length); //index�œG���X�|�[������ _spawnPoint �������_���Ŏw�肷��
                Vector3 pos =new Vector3(_spawnPoint[index].position.x, 1, _spawnPoint[index].position.z) ; //�X�|�[��������ꏊ���w��
                var go = Instantiate(_enemyPrefab, pos, Quaternion.identity); //�G�� Instantiate ����
                _enemies.Add(go); //�o�������G�� List �ɒǉ�����(�G�̕������𐧌����邽��)
                _timer = 0; //�������Ԃ����ɖ߂�
            }
        }
    }
}
