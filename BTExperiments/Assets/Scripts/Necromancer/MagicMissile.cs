using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    public float maxLiveTime = 9f;
    public float speed;
    public float rotationSpeed;

    private float _timer;
    private Vector3 _groundY;
    private GameObject _player;

    void Start()
    {
        _groundY.y = transform.position.y;

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 plyaerLocation = _player.transform.position + _player.transform.forward;
        
        
        Vector3 missileRotation = plyaerLocation - transform.position;
        missileRotation.y = 0;
        transform.rotation = Quaternion.LookRotation(missileRotation);


        Vector3 missileMovement = Vector3.MoveTowards(transform.position, plyaerLocation, speed * Time.deltaTime);
        transform.position = new Vector3(missileMovement.x, _groundY.y, missileMovement.z);
        


    }
}
