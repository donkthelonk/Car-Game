using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Truck : Vehicle
{
    private bool isQuirky = false;

    // Start is called before the first frame update
    void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isQuirky)
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
        Debug.Log("Truck Honk() called!");

        // play the audio clip
        vehicleAudio.Play();
    }

    protected override void Quirk()
    {
        Debug.Log("Truck Quirk() called!");

        int rotationSpeed = 1000;

        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
    }
}
