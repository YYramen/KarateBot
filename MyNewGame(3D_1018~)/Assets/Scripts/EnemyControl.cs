using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    [SerializeField] GameObject _player = default;
    NavMeshAgent _navMeshAgent;
    [SerializeField] float _enemyZone = 3f;
    //[SerializeField] float _moveSpeed = 0.1f;
    float _distance = 0f;
    //float firstY = 0;
    // Start is called before the first frame update
    void Start()
    {
        //firstY = gameObject.transform.position.y;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector3.Distance(this.transform.position, _player.transform.position);
        if (_distance < _enemyZone)
        {
            _navMeshAgent.destination = _player.transform.position;
        }
            //{
            //    Vector3 dir = default;

            //    if (_player)
            //    {
            //        dir = _player.transform.position - this.transform.position;
            //    }


            //this.transform.Translate(new Vector3(dir.x ,0 , dir.z) * _moveSpeed);//ƒvƒŒƒCƒ„[‚É‹ß‚Ã‚­ˆ—
            
    }
}
