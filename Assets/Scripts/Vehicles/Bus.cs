using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bus : Vehicle
{
    [SerializeField] private float speed = 100.0f;

    private bool isQuirky = false;
    private Rigidbody vehicleRb;

    // Start is called before the first frame update
    void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();
        vehicleRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuirky)
        {
            Quirk();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // when Vehicle collides with the player object
        if (collision.gameObject.CompareTag("Player"))
        {
            isQuirky = true;
            Honk();
        }
    }

    protected override void Honk()
    {
        Debug.Log("Bus Honk() called!");

        // play the audio clip
        vehicleAudio.Play();
    }

    protected override void Quirk()
    {
        Debug.Log("Bus Quirk() called!");

        vehicleRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }
}
