using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject powerupPrefab;

    [SerializeField] [Range(0f, 1f)] private float powerupDropChance = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

            if (powerupPrefab != null && Random.value < powerupDropChance)
            {
                Debug.Log("Crate dropped a powerup!");
                Instantiate(powerupPrefab, transform.position, powerupPrefab.transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
