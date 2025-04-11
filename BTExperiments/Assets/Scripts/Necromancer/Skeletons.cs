using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Tasks.Actions;
using UnityEngine;
using UnityEngine.AI;

public class Skeletons : MonoBehaviour 
{
    private GameObject _player;
    private NavMeshAgent _agent;

    public float maxLiveTime = 9f;
    private float _timer;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _agent = GetComponent<NavMeshAgent>(); // because this is not a BB, I've used this to get access to NavMesh of the skeleton

    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_player.transform.position); // seting destination for skeletons to follow player
        
        _timer += Time.deltaTime;
        if (_timer >= maxLiveTime){
            Destroy(gameObject);
        }


    }
}
