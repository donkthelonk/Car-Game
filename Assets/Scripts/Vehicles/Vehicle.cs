using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE base class
abstract class Vehicle : MonoBehaviour
{
    protected AudioSource vehicleAudio;

    public AudioClip honkClip;

    // ABSTRACTION abstract function
    protected abstract void Quirk();

    // Start is called before the first frame update
    void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();

        Move();
        ChangeLanes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // when Vehicle collides with the player object
        if(collision.gameObject.CompareTag("Player"))
        {
            Honk();
        }
    }

    // POLYMORPHISM virtual function 
    protected virtual void ChangeLanes()
    {
        Debug.Log("Vehicle ChangeLanes() called!");
    }

    // POLYMORPHISM virtual function 
    protected virtual void Honk()
    {
        Debug.Log("Vehicle Honk() called!");

        // play the audio clip once at 50% volume
        vehicleAudio.PlayOneShot(honkClip, 0.5f);
    }

    void Move()
    {
        Debug.Log("Vehicle Move() called!");
    }
}
