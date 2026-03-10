using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverScreen;
    public Timer timer;

    [SerializeField] private int maxHealth = 3;

    private int score;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        InitializeScore();
        InitializeHealth();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to initialize the score when the game scene starts
    void InitializeScore()
    {
        score = 0;
        UpdateScore(0);
    }

    // Method to initialize health when the game scene starts
    void InitializeHealth()
    {
        health = maxHealth;
        healthText.text = "Health: " + health;
    }

    // Method to decrease health and end the game if health reaches 0
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthText.text = "Health: " + health;
        StartCoroutine(FlashHealthText());

        if (health <= 0)
            EndGame();
    }

    IEnumerator FlashHealthText()
    {
        healthText.text = "Health: <color=red><size=125%>" + health + "</size></color>";
        yield return new WaitForSeconds(0.3f);
        healthText.text = "Health: " + health;
    }

    // Method to add time to the timer
    public void AddTime(float amount)
    {
        timer.AddTime(amount);
    }

    // Method to restore health by 1 up to maxHealth
    public void RestoreHealth()
    {
        health = Mathf.Min(health + 1, maxHealth);
        healthText.text = "Health: " + health;
    }

    // Method to Update the score and display the updated score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (scoreToAdd > 0)
            StartCoroutine(FlashScoreText());
    }

    IEnumerator FlashScoreText()
    {
        scoreText.text = "Score: <color=green><size=125%>" + score + "</size></color>";
        yield return new WaitForSeconds(0.3f);
        scoreText.text = "Score: " + score;
    }

    // Method to end the game
    public void EndGame()
    {
        Debug.Log("Game Over!");
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Score: " + score;
        Time.timeScale = 0;
    }

    // Method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
