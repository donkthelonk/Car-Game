using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;

    private Rigidbody playerRb;

    public float xRange = 15;
    public float zMax = 10;
    public float zMin = -2;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        // Get input from user and move player
        MovePlayer();

        // Constrain movement on the z axis
        ConstrainPlayerPosition();
    }

    // Moves player based on user input
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * speed * verticalInput, ForceMode.Impulse);
        playerRb.AddForce(Vector3.right * speed * horizontalInput, ForceMode.Impulse);
    }

    // Constrains player movement 
    void ConstrainPlayerPosition()
    {
        if (transform.position.z > zMax)
        {
            // Constrain player to zMax
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
            // Stop velocity
            playerRb.velocity = Vector3.zero;
        }

        if (transform.position.z < zMin)
        {
            // Constrain player to zMin
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
            // Stop velocity
            playerRb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bus"))
        {
            Debug.Log("Player has collided with bus.");
        }
        else if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Player has collided with car.");
        }
        else if (collision.gameObject.CompareTag("Crate"))
        {
            Debug.Log("Player has collided with crate.");
        }
    }
}
