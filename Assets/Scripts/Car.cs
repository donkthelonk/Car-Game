using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    private void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();

        ChangeLanes();
    }

    public override void ChangeLanes()
    {
        Debug.Log("Car ChangeLanes() called!");
        base.ChangeLanes();
    }

    void Honk()
    {
        Debug.Log("Car Honk() called!");

        // play the audio clip once at 50% volume
        vehicleAudio.PlayOneShot(honkClip, 0.5f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // when Vehicle collides with the player object
        if (collision.gameObject.CompareTag("Player"))
        {
            Honk();
        }
    }
}
