using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Tasks.Actions;
using UnityEngine;
using UnityEngine.AI;

public class Skeletons : MonoBehaviour 
{
    public float maxLiveTime = 9f;

    private float _timer;
    private GameObject _player;
    private NavMeshAgent _agent;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_player.transform.position);
        
        _timer += Time.deltaTime;
        if (_timer >= maxLiveTime){
            Destroy(gameObject);
        }


    }
}
