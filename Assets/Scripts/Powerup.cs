using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType { Health, Invincibility, ScoreMultiplier }

public class Powerup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100.0f;
    public PowerupType type = PowerupType.Health;
    public AudioClip pickupSound;

    void Start()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        if (r != null)
        {
            switch (type)
            {
                case PowerupType.Health:         r.material.color = Color.green; break;
                case PowerupType.Invincibility:  r.material.color = Color.blue;  break;
                case PowerupType.ScoreMultiplier: r.material.color = Color.red;  break;
            }
        }
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
