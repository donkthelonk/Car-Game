using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float speed = 10.0f;

    private Rigidbody playerRb;
    private GameManager gameManager;
    private bool isInvincible = false;

    [SerializeField] private float xRange = 15;
    [SerializeField] private float zMax = 10;
    [SerializeField] private float zMin = -4;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
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

    void ResetInvincibility()
    {
        isInvincible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy Powerup and apply bonus
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            gameManager.RestoreHealth();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Traffic") && !isInvincible)
        {
            Debug.Log("Player has collided with traffic.");
            isInvincible = true;
            Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
            gameManager.TakeDamage(vehicle != null ? vehicle.damageAmount : 1);
            Invoke("ResetInvincibility", 0.5f);
        }
        else if (collision.gameObject.CompareTag("Crate"))
        {
            Debug.Log("Player has collided with a crate.");
        }
    }
}
