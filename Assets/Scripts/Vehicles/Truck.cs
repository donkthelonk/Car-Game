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
        Debug.Log("Truck Honk() called!");
        AudioSource.PlayClipAtPoint(honkClip, transform.position);
    }

}
