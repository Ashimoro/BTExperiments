using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashSpawner : MonoBehaviour
{
    
    public float maxSpawnRadius = 10f;
    public float maxTrashSpawned = 3;
    public GameObject trashPrefab;

    private float _currentTrashAmmount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (_currentTrashAmmount <= maxTrashSpawned - 1) {

            Spawner();

        }

    }

    void Spawner(){

        Vector2 spawnPoint = Random.insideUnitCircle * maxSpawnRadius;

        Instantiate(trashPrefab, new Vector3(spawnPoint.x,1,spawnPoint.y), Quaternion.identity);

        _currentTrashAmmount++;

    }

}
