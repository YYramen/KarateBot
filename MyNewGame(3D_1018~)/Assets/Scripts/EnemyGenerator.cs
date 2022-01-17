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
            if (_spawnInterval < _timer && _enemies.Count<_spawnPoint.Length)//時間制限が経ち,スポーン上限でないとき
            {
                int index = Random.Range(0, _spawnPoint.Length); //indexは敵がスポーンする場所のこと
                Vector3 pos =new Vector3(_spawnPoint[index].position.x, 1, _spawnPoint[index].position.z) ;
                var go = Instantiate(_enemyPrefab, pos, Quaternion.identity);
                _enemies.Add(go);
                _timer = 0;
            }
        }
    }
}
