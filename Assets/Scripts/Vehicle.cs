using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    //[SerializeField] private float speed;
    //[SerializeField] private int mass;
    public AudioSource vehicleAudio;

    // Start is called before the first frame update
    void Start()
    {
        vehicleAudio = GetComponent<AudioSource>();

        Honk();
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
            vehicleAudio.Play();
        }
    }

    public virtual void ChangeLanes()
    {
        Debug.Log("Vehicle ChangeLanes() called!");
    }

    void Honk()
    {
        Debug.Log("Vehicle Honk() called!");
    }

    void Move()
    {
        Debug.Log("Vehicle Move() called!");
    }
}
