using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵をスポーンさせるためのコンポーネント
/// </summary>

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab = default; // スポーンさせる敵のプレハブ
    [SerializeField] Transform[] _spawnPoint = default; // スポーンさせる場所
    [SerializeField] float _spawnInterval = 3f; // スポーンさせる間隔
    List<GameObject> _enemies = new List<GameObject>(); // 沸き数を制限するため敵を格納するリスト
    float _timer = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag =="Player")
        {
            _timer += Time.deltaTime;
            if (_spawnInterval < _timer && _enemies.Count<_spawnPoint.Length) //時間制限が経ち,スポーン上限でないとき
            {
                int index = Random.Range(0, _spawnPoint.Length); //indexで敵がスポーンする _spawnPoint をランダムで指定する
                Vector3 pos =new Vector3(_spawnPoint[index].position.x, 1, _spawnPoint[index].position.z) ; //スポーンをする場所を指定
                var go = Instantiate(_enemyPrefab, pos, Quaternion.identity); //敵を Instantiate する
                _enemies.Add(go); //出現した敵を List に追加する(敵の沸き数を制限するため)
                _timer = 0; //制限時間を元に戻す
            }
        }
    }
}
