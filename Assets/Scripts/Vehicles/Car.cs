using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    private bool isQuirky = false;

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

    public override void ChangeLanes()
    {
        Debug.Log("Car ChangeLanes() called!");
        base.ChangeLanes();
    }

    protected override void Honk()
    {
        Debug.Log("Car Honk() called!");

        // play the audio clip once at 50% volume
        //vehicleAudio.PlayOneShot(honkClip, 0.5f);

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

    protected override void Quirk()
    {
        Debug.Log("Car Quirk() called!");

        int rotationSpeed = 1000;

        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
    }
}
