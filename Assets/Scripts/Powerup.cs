using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    // Method to rotate the powerup as it moves down the screen
    void Rotate()
    {
        transform.Rotate(Vector3.right * (rotationSpeed * Time.deltaTime));
    }
}
