using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 5f;
    private float _rotationSpeed = 720f;

    // Update is called once per frame
    void Update()
    {
        
        float horizontaInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 _direction = new Vector3(horizontaInput, 0f, verticalInput);

        if (_direction.magnitude > 0.1f) {

            _direction.Normalize();

            transform.Translate(_direction * speed * Time.deltaTime, Space.World);
            Quaternion _rotation = Quaternion.LookRotation(_direction,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);

        }


    }
}
