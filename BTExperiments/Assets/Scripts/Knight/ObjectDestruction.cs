using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

		if (collision.gameObject.CompareTag("Obstacles")){
            UnityEngine.Object.Destroy(collision.gameObject);

        }

    }
}
