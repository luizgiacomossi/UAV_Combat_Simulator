using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100.0f;
    public float jumpForce = 100f;
    public bool isOnGround = true;
    public bool gameOver = false;
    public float timeScale;

    public float yBound = 6;

    private Vector3 initialPos;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
                Time.timeScale = timeScale;

        MovePlayer();
        RestrictMovement();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

    }

    // moves the player based on inputs
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //float jumpInput = Input.GetAxis("Jump");
        float jumpInput  = 0f;
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
        playerRb.AddForce(Vector3.up * jumpForce * jumpInput);//, ForceMode.Impulse);
        //playerRb.AddForceAtPosition( transform.TransformVector( Vector3.up) * jumpForce * jumpInput, transform.position + transform.TransformVector( new Vector3(-0.6064265f,0, -0.6064247f)) );//, ForceMode.Impulse);


        
        //AddForceAtPosition(Vector3 force, Vector3 position, ForceMode mode = ForceMode.Force);


        transform.Rotate(Vector3.forward*-1, horizontalInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.right, verticalInput * speed * Time.deltaTime);
    }

    // restricts the player movement
    void RestrictMovement()
    {
        if(transform.position.y > yBound){
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
            isOnGround = false;

        }
        if(transform.position.y < -10){
            transform.position = initialPos;
        }
    }
    void Reset(){
        transform.position = initialPos;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("Obstacle")){
        }
    }

}
