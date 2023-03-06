using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    //[SerializeField] private float speed;
    //[SerializeField] private int mass;
    protected AudioSource vehicleAudio;

    public AudioClip honkClip;

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

    public virtual void ChangeLanes()
    {
        Debug.Log("Vehicle ChangeLanes() called!");
    }

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

    protected virtual void Quirk()
    {
        // no quirk for base class
    }
}
