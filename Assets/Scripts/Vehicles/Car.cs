using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE derived class
class Car : Vehicle
{
    private int number = 0;

    // ENCAPSULATION private variable setter
    public void SetNumber(int newNumber)
    {
        number = newNumber;
    }

    // ENCAPSULATION private variable getter
    public int GetNumber()
    {
        return number;
    }

private void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();
    }

    // POLYMORPHISM function override
    protected override void Honk()
    {
        Debug.Log("Car Honk() called!");
        AudioSource.PlayClipAtPoint(honkClip, transform.position);
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
}
