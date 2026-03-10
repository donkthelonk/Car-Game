using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject[] powerupPrefabs;

    [SerializeField] [Range(0f, 1f)] private float powerupDropChance = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

            if (powerupPrefabs.Length > 0 && Random.value < powerupDropChance)
            {
                Debug.Log("Crate dropped a powerup!");
                GameObject randomPowerup = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];
                Instantiate(randomPowerup, transform.position, randomPowerup.transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
