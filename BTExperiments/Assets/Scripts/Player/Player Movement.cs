using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float playerHP = 3;
    

    public float jumpForce = 5f;
    private bool _isGrounded;


    public float speed = 5f;
    private float _rotationSpeed = 720f;
    private Vector3 _movementDirection;
    private Rigidbody _rb;
    private bool _canControl;


    public float knockbackTime = 0.1f;
    public float knockbackForce = 10f;
    

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

    
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dashTimer <= 0){
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
        if (collision.gameObject.CompareTag("Ground")){

            _isGrounded = true;  
        }

        if (collision.gameObject.CompareTag("Knight") || collision.gameObject.CompareTag("Skeleton")){
            Debug.Log("123");
            playerHP--;
            Rigidbody enemy = collision.gameObject.GetComponent<Rigidbody>();

            if(enemy != null){

                Vector3 knockbackDistance = transform.position - enemy.transform.position;
                knockbackDistance.y = 0;
                knockbackDistance = knockbackDistance.normalized * knockbackForce;
                _rb.AddForce(knockbackDistance, ForceMode.Impulse);
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

        _canControl = false;
        yield return new WaitForSeconds(knockbackTime);
        _rb.velocity = Vector3.zero;
        _canControl = true;
    }

}
