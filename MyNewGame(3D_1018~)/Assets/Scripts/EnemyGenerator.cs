using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab = default;
    [SerializeField] Transform[] _spawnPoint = default;
    [SerializeField] float _spawnInterval = 3f;
    List<GameObject> _enemies = new List<GameObject>();
    float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag =="Player")
        {
            _timer += Time.deltaTime;
            if (_spawnInterval < _timer && _enemies.Count<_spawnPoint.Length) //���Ԑ������o��,�X�|�[������łȂ��Ƃ�
            {
                int index = Random.Range(0, _spawnPoint.Length); //index�œG���X�|�[������ _spawnPoint �������_���Ŏw�肷��
                Vector3 pos =new Vector3(_spawnPoint[index].position.x, 2, _spawnPoint[index].position.z) ; //�X�|�[��������ꏊ���w��
                var go = Instantiate(_enemyPrefab, pos, Quaternion.identity); //�G�� Instantiate ����
                _enemies.Add(go); //�o�������G�� List �ɒǉ�����(�G�̕������𐧌����邽��)
                _timer = 0; //�������Ԃ����ɖ߂�
            }
        }
    }
}
