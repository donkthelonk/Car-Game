using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float speed = 10.0f;

    private Rigidbody playerRb;
    private GameManager gameManager;
    private Renderer[] playerRenderers;
    private Color[] originalColors;
    private bool isInvincible = false;

    [SerializeField] private float xRange = 15;
    [SerializeField] private float zMax = 10;
    [SerializeField] private float zMin = -4;

    [SerializeField] private float tiltAngle = 15f;
    [SerializeField] private float tiltSpeed = 5f;

    [SerializeField] private float controlsRandomizeInterval = 5.0f;
    public TextMeshProUGUI scrambledText;
    private int horizontalMultiplier = 1;
    private int verticalMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        playerRenderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[playerRenderers.Length];
        for (int i = 0; i < playerRenderers.Length; i++)
            originalColors[i] = playerRenderers[i].material.color;
        InvokeRepeating("RandomizeControls", controlsRandomizeInterval, controlsRandomizeInterval);
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        // Get input from user and move player
        MovePlayer();

        // Tilt player when moving horizontally
        TiltPlayer();

        // Constrain movement on the z axis
        ConstrainPlayerPosition();
    }

    // Moves player based on user input
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerRb.velocity = new Vector3(horizontalInput * horizontalMultiplier * speed, playerRb.velocity.y, verticalInput * verticalMultiplier * speed);
    }

    // Randomly inverts one or both control axes for 1 second
    void RandomizeControls()
    {
        horizontalMultiplier = Random.value > 0.5f ? -1 : 1;
        verticalMultiplier = Random.value > 0.5f ? -1 : 1;
        scrambledText.gameObject.SetActive(true);
        StartCoroutine(RainbowText());
        StartCoroutine(RainbowPlayer());
        Invoke("ResetControls", 1.0f);
    }

    IEnumerator RainbowPlayer()
    {
        float hue = 0f;
        while (scrambledText.gameObject.activeSelf)
        {
            hue = (hue + Time.deltaTime * 2f) % 1f;
            Color rainbow = Color.HSVToRGB(hue, 1f, 1f);
            foreach (Renderer r in playerRenderers)
                r.material.color = rainbow;
            yield return null;
        }
        for (int i = 0; i < playerRenderers.Length; i++)
            playerRenderers[i].material.color = originalColors[i];
    }

    IEnumerator RainbowText()
    {
        float hue = 0f;
        while (scrambledText.gameObject.activeSelf)
        {
            hue = (hue + Time.deltaTime * 2f) % 1f;
            scrambledText.color = Color.HSVToRGB(hue, 1f, 1f);
            yield return null;
        }
    }

    void ResetControls()
    {
        horizontalMultiplier = 1;
        verticalMultiplier = 1;
        scrambledText.gameObject.SetActive(false);
    }

    // Tilts player left/right based on horizontal input
    void TiltPlayer()
    {
        Quaternion targetRotation = Quaternion.Euler(0, horizontalInput * tiltAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    // Constrains player movement
    void ConstrainPlayerPosition()
    {
        if (transform.position.z > zMax)
        {
            // Constrain player to zMax
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
            // Stop velocity
            playerRb.velocity = Vector3.zero;
        }

        if (transform.position.z < zMin)
        {
            // Constrain player to zMin
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
            // Stop velocity
            playerRb.velocity = Vector3.zero;
        }
    }

    void ResetInvincibility()
    {
        isInvincible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy Powerup and apply bonus
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            gameManager.RestoreHealth();
            gameManager.AddTime(5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Traffic") && !isInvincible)
        {
            Debug.Log("Player has collided with traffic.");
            isInvincible = true;
            Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
            gameManager.TakeDamage(vehicle != null ? vehicle.damageAmount : 1);
            Invoke("ResetInvincibility", 0.5f);
        }
        else if (collision.gameObject.CompareTag("Crate"))
        {
            Debug.Log("Player has collided with a crate.");
        }
    }
}
