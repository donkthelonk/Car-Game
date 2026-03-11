using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Truck : Vehicle
{
    // Start is called before the first frame update
    void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();
        damageAmount = 2;
        pointValue = 20;
    }

    protected override void Honk()
    {
        Debug.Log("Truck Honk() called!");
        AudioSource.PlayClipAtPoint(honkClip, transform.position);
    }

}
