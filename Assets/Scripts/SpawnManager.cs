using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] traffic;
    public GameObject powerup;

    private float zTrafficSpawn = 15.0f;
    private float zPowerupSpawn = 15.0f;
    private float xSpawnRange = 10.0f;
    //private float zPowerupRange = 5.0f;   // only used if powerup spawns in the road
    private float yTrafficSpawn = 0.5f;
    private float yPowerupSpawn = 0.25f;

    private float powerupSpawnTime = 5.0f;
    private float trafficSpawnTime = 1.0f;
    private float startDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Continuously spawn traffic at trafficSpawnTime intervals
        InvokeRepeating("SpawnRandomTraffic", startDelay, trafficSpawnTime);

        // Continuously spawn powerups at powerupSpawnTime intervals
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn random traffic at top of screen
    void SpawnRandomTraffic()
    {
        // Create random spawnPos x value
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        // Create random array index
        int randomIndex = Random.Range(0, traffic.Length);

        // Create vector3 for spawnPos
        Vector3 spawnPos = new Vector3(randomX, yTrafficSpawn, zTrafficSpawn);

        // Spawn random traffic at top of screen
        Instantiate(traffic[randomIndex], spawnPos, traffic[randomIndex].gameObject.transform.rotation);
    }

    // Spawn powerup at top of screen
    void SpawnPowerup()
    {
        // Create random spawnPos x value
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        // Create vector3 for spawnPos
        Vector3 spawnPos = new Vector3(randomX, yPowerupSpawn, zPowerupSpawn);

        // Spawn powerup at top of screen
        Instantiate(powerup, spawnPos, powerup.transform.rotation);
    }
}
