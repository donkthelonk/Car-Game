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
    void Update()
    {
        //Keep the player in bounds
        // replaced the x bounds with physical walls
        //if (transform.position.x < -xRange)
        //    transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);

        //if (transform.position.x > xRange)
        //    transform.position = new Vector3(xRange, transform.position.y, transform.position.z);

        if (transform.position.z > zMax)
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax);

        if (transform.position.z < zMin)
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin);

        // Get input from user move player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

    }
}
