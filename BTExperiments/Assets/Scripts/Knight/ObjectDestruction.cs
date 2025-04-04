using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
		if (collision.gameObject.CompareTag("Obstacles")){

            Destroy(collision.gameObject);

        }

    }
}
