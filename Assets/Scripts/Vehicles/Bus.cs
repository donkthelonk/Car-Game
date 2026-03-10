using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bus : Vehicle
{
    private Rigidbody vehicleRb;

    // Start is called before the first frame update
    void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();
        vehicleRb = GetComponent<Rigidbody>();
    }

private void OnCollisionEnter(Collision collision)
    {
        // when Vehicle collides with the player object
        if (collision.gameObject.CompareTag("Player"))
        {
            Honk();
            Explode();
        }
    }

    protected override void Honk()
    {
        Debug.Log("Bus Honk() called!");
        AudioSource.PlayClipAtPoint(honkClip, transform.position);
    }

}
