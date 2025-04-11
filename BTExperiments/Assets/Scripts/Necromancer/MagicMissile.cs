using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    public float speed;
    private Vector3 _groundY;
    private GameObject _player;

    public float maxLiveTime = 5f;
    private float _timer;
    void Start()
    {
        _groundY.y = transform.position.y; // fixating y coordinate for future

        _player = GameObject.FindGameObjectWithTag("Player"); // finding player to follow it

        _timer = 0f;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= maxLiveTime){
            Destroy(gameObject); // timer to destroy missile to not overload the player in the game
        }

        Vector3 plyaerLocation = _player.transform.position + _player.transform.forward; // finding location of the player and doing it a little bit in front to make the missile not just follow player as skeletons
        
        
        Vector3 missileRotation = plyaerLocation - transform.position; 
        missileRotation.y = 0;
        transform.rotation = Quaternion.LookRotation(missileRotation); // rotating missile to the location of flying


        Vector3 missileMovement = Vector3.MoveTowards(transform.position, plyaerLocation, speed * Time.deltaTime); 
        transform.position = new Vector3(missileMovement.x, _groundY.y, missileMovement.z);   // movement of the missile
        


    }
}
