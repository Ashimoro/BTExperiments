using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ObjectDestruction : MonoBehaviour
{

    public GameObject ground;
    
    private NavMeshSurface _groundNM;

    void Start()
    {
        _groundNM = ground.GetComponent<NavMeshSurface>();
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
        _groundNM.BuildNavMesh();
    }
}
