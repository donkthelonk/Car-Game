using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    [SerializeField] private float zDestroy = -10.0f;
    [SerializeField] private Rigidbody objectRb;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move object down the screen
        objectRb.AddForce(Vector3.forward * -speed);

        // Destroy object when it goes off screen
        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }
}
