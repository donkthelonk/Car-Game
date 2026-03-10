using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE base class
abstract class Vehicle : MonoBehaviour
{
    protected AudioSource vehicleAudio;
    protected GameManager gameManager;

    public AudioClip honkClip;
    public GameObject explosionPrefab;
    [SerializeField] protected int pointValue = 10;

    // ABSTRACTION abstract function
    protected abstract void Quirk();

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

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
            Explode();
        }
    }

    protected void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        gameManager.UpdateScore(pointValue);
        Destroy(gameObject);
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
