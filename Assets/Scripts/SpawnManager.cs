using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private bool spawnTraffic = true;
    public GameObject[] traffic;
    public GameObject[] powerups;
    public GameObject crate;
    public GameManager gameManager;

    [SerializeField] private float zTrafficSpawn = 15.0f;
    [SerializeField] private float zPowerupSpawn = 15.0f;
    [SerializeField] private float zCrateSpawn = 15.0f;
    [SerializeField] private float xSpawnRange = 12.0f;
    //[SerializeField] private float zPowerupRange = 5.0f;   // only used if powerup spawns in the road
    [SerializeField] private float yTrafficSpawn = 0.5f;
    [SerializeField] private float yPowerupSpawn = 0.25f;
    [SerializeField] private float yCrateSpawn = 0.25f;

    [SerializeField] private float trafficSpawnTime = 1.0f;
    [SerializeField] private float powerupSpawnTime = 10.0f;
    [SerializeField] private float crateSpawnTime = 5.0f;
    [SerializeField] private float startDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Continuously spawn traffic at trafficSpawnTime intervals
        InvokeRepeating("SpawnRandomTraffic", startDelay, trafficSpawnTime/2);

        // Continuously spawn powerups at powerupSpawnTime intervals
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);

        // Continuously spawn powerups at crateSpawnTime intervals
        InvokeRepeating("SpawnCrate", startDelay, crateSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn random traffic at top of screen
    void SpawnRandomTraffic()
    {
        if (!spawnTraffic) return;

        // Create random spawnPos x value
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        // Create random array index
        int randomIndex = Random.Range(0, traffic.Length);

        // Create vector3 for spawnPos
        Vector3 spawnPos = new Vector3(randomX, yTrafficSpawn, zTrafficSpawn);

        // Spawn random traffic at top of screen
        Instantiate(traffic[randomIndex], spawnPos, traffic[randomIndex].gameObject.transform.rotation);

        // Update Score
        gameManager.UpdateScore(5);
    }

    // Spawn powerup at top of screen
    void SpawnPowerup()
    {
        // Create random spawnPos x value
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        // Create vector3 for spawnPos
        Vector3 spawnPos = new Vector3(randomX, yPowerupSpawn, zPowerupSpawn);

        // Spawn random powerup at top of screen
        GameObject randomPowerup = powerups[Random.Range(0, powerups.Length)];
        Instantiate(randomPowerup, spawnPos, randomPowerup.transform.rotation);
    }

    // Spawn crate at top of screen
    void SpawnCrate()
    {
        // Create random spawnPos x value
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        // Create vector3 for spawnPos
        Vector3 spawnPos = new Vector3(randomX, yCrateSpawn, zCrateSpawn);

        // Spawn crate at top of screen
        Instantiate(crate, spawnPos, crate.transform.rotation);
    }
}
