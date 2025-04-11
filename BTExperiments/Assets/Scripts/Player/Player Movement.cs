using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // player movement script;
    
    // player HP;
    public float playerHP = 3;
    

    // everything related to jumping
    public float jumpForce = 5f;
    private bool _isGrounded;


    // everything related to player movement
    public float speed = 5f;
    private float _rotationSpeed = 720f;
    private Vector3 _movementDirection;
    private Rigidbody _rb;
    private bool _canControl;


    // everything related to knockback
    public float knockbackTime = 0.1f;
    public float knockbackForce = 10f;
    

    // everything related to dashing
    public float dashDistance = 3f;
    public float dashDelay = 2;
    private float _dashTimer = 0f;
    
        void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _canControl = true;
    }


    void Update()
    {
        if (_canControl) {

        float horizontaInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(horizontaInput,0f,verticalInput); // Getting palyer inputs into a single vector3 variable



        if (Input.GetKeyDown("space") && _isGrounded) {
 
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Player jumping

        }

    
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dashTimer <= 0){ // dashing input
            Vector3 dashPosition = _movementDirection * dashDistance;
            _rb.AddForce(dashPosition, ForceMode.Impulse);
            _dashTimer = dashDelay;
        }

        if (_dashTimer >= 0) {
            _dashTimer -= Time.deltaTime;
        }

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
        if (collision.gameObject.CompareTag("Ground")){ // checking collision for jumping

            _isGrounded = true;  
        }

        if (collision.gameObject.CompareTag("Knight") || collision.gameObject.CompareTag("Skeleton") || collision.gameObject.CompareTag("Missile")){ // checking if player is colliding with any enemy
            Debug.Log("123");
            Rigidbody enemy = collision.gameObject.GetComponent<Rigidbody>(); // receiving rigibody to use it for transform.position (I've tried without it, and I had some bugs)

            if(enemy != null){

                Vector3 knockbackDistance = transform.position - enemy.transform.position; // where player will fly after the hit
                knockbackDistance.y = 0; // preventing of y coordinate change
                knockbackDistance = knockbackDistance.normalized * knockbackForce; // how far player will fly
                _rb.AddForce(knockbackDistance, ForceMode.Impulse); // sending player to the flight

                    if(collision.gameObject.CompareTag("Skeleton") || collision.gameObject.CompareTag("Missile")){ // if missile or skeleton is touching the player - they will be destroyed
                        Destroy(collision.gameObject);
                    }

                StartCoroutine(knockbackTimer());
                
            }
        }    
    }


    void OnCollisionExit(Collision collision)
    {
         if (collision.gameObject.CompareTag("Ground")){

            _isGrounded = false;

        }
    }

    private IEnumerator knockbackTimer(){

        _canControl = false; // removing control from the player, to not reset the velocity during knockback
        yield return new WaitForSeconds(knockbackTime);
        playerHP--;
        _rb.velocity = Vector3.zero;
        _canControl = true;
    }

}
