using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE base class
abstract class Vehicle : MonoBehaviour
{
    protected GameManager gameManager;

    public AudioClip explosionClip;
    public GameObject explosionPrefab;
    public GameObject floatingTextPrefab;
    [SerializeField] protected int pointValue = 10;
    public int damageAmount = 1;

void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            Explode();
        }
    }

    protected void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        if (explosionClip != null)
            AudioSource.PlayClipAtPoint(explosionClip, transform.position);

        if (floatingTextPrefab != null)
        {
            GameObject textObj = Instantiate(floatingTextPrefab, transform.position + Vector3.up, Quaternion.identity);
            textObj.GetComponent<FloatingText>().SetText("+" + pointValue + "pts");
        }

        gameManager.UpdateScore(pointValue);
        gameManager.AddTime(1f);
        Destroy(gameObject);
    }

}
