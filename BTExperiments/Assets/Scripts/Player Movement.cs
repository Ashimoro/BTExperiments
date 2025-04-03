using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 5f;
    public float jumpForce = 5f;
    
    private float _rotationSpeed = 720f;
    private Rigidbody _rb;
    private bool _isGrounded;
    private Vector3 _movementDirection;
    
        void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        
        float horizontaInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(horizontaInput,0f,verticalInput); // Getting palyer inputs into a single vector3 variable



        if (Input.GetKeyDown("space") && _isGrounded) {

            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Player jumping

        }

    }


    void FixedUpdate()
    {
        if (_movementDirection.magnitude > 0.1f) {
            Vector3 move = _movementDirection * speed * Time.fixedDeltaTime; // Adding to the inputs player speed and using time.fixedDeltaTime, to make everything smoother;
            _rb.MovePosition(_rb.position + move); // Player movement

            Quaternion targetPosition = Quaternion.LookRotation(_movementDirection, Vector3.up); //Finding position where player should be rotated
            Quaternion rotation = Quaternion.RotateTowards(_rb.rotation, targetPosition, _rotationSpeed * Time.fixedDeltaTime); //Making rotation smoother and not instant
            _rb.MoveRotation(rotation); //Rotating player to the direction of movement

        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){

            _isGrounded = true;

        }
    }


    void OnCollisionExit(Collision collision)
    {
         if (collision.gameObject.CompareTag("Ground")){

            _isGrounded = false;

        }
    }
}
