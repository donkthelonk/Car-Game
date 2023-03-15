using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE derived class
class Car : Vehicle
{
    private bool isQuirky = false;

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

    // ABSTRACTION function override
    protected override void Quirk()
    {
        Debug.Log("Car Quirk() called!");

        int rotationSpeed = 1000;

        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
    }

    private void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();

        ChangeLanes();
    }

    private void Update()
    {
        if (isQuirky)
        {
            Quirk();
        }
    }

    // POLYMORPHISM function override
    protected override void ChangeLanes()
    {
        Debug.Log("Car ChangeLanes() called!");
        base.ChangeLanes();
    }

    // POLYMORPHISM function override
    protected override void Honk()
    {
        Debug.Log("Car Honk() called!");
        vehicleAudio.Play();

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
}
