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
        damageAmount = 3;
        pointValue = 30;
    }

    protected override void Honk()
    {
        Debug.Log("Bus Honk() called!");
        AudioSource.PlayClipAtPoint(honkClip, transform.position);
    }

}
