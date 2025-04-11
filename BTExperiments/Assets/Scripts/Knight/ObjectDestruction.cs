using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ObjectDestruction : MonoBehaviour
{

    // simple script to destroy the obstacles and recreate nav mesh every time when knight is coliding with something

    public GameObject ground;
    
    private NavMeshSurface _groundNM;

    void Start()
    {
        _groundNM = ground.GetComponent<NavMeshSurface>(); // getting a navmesh component from the ground
    }
    void OnCollisionEnter(Collision collision)
    {
        
		if (collision.gameObject.CompareTag("Obstacles")){

            Destroy(collision.gameObject);

            StartCoroutine(navMeshUpdate());
        }

    }

    private IEnumerator navMeshUpdate(){

        yield return new WaitForSeconds(0.1f);
        _groundNM.BuildNavMesh(); // updating navmesh after small delay. This is required to avoid bugs
    }
}
