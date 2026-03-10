using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreValueText;
    public TextMeshProUGUI healthValueText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    public Timer timer;
    public PowerupText powerupText;

    private const string HighScoreKey = "HighScore";

    [SerializeField] private int maxHealth = 3;

    private int score;
    private int health;
    private int scoreMultiplier = 1;
    private float scoreValueFontSize;
    private float healthValueFontSize;

    // Start is called before the first frame update
    void Start()
    {
        scoreValueFontSize = scoreValueText.fontSize;
        healthValueFontSize = healthValueText.fontSize;
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
        healthValueText.text = health.ToString();
    }

    // Method to decrease health and end the game if health reaches 0
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthValueText.text = health.ToString();
        StartCoroutine(FlashHealthText());

        if (health <= 0)
            EndGame();
    }

    IEnumerator FlashHealthText()
    {
        healthValueText.fontSize = healthValueFontSize * 1.1f;
        healthValueText.text = "<color=red>" + health + "</color>";
        yield return new WaitForSeconds(0.3f);
        healthValueText.fontSize = healthValueFontSize;
        healthValueText.text = health.ToString();
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
        healthValueText.text = health.ToString();
    }

    // Method to show powerup pickup text
    public void ShowPowerupText(string message, Color color)
    {
        if (powerupText != null)
            powerupText.Show(message, color);
    }

    // Method to activate a score multiplier for a duration
    public void ActivateScoreMultiplier(int multiplier, float duration)
    {
        scoreMultiplier = multiplier;
        StartCoroutine(ResetScoreMultiplier(duration));
    }

    IEnumerator ResetScoreMultiplier(float duration)
    {
        yield return new WaitForSeconds(duration);
        scoreMultiplier = 1;
    }

    // Method to Update the score and display the updated score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd * scoreMultiplier;
        scoreValueText.text = score.ToString();
        if (scoreToAdd > 0)
            StartCoroutine(FlashScoreText());
    }

    IEnumerator FlashScoreText()
    {
        scoreValueText.fontSize = scoreValueFontSize * 1.1f;
        scoreValueText.text = "<color=green>" + score + "</color>";
        yield return new WaitForSeconds(0.3f);
        scoreValueText.fontSize = scoreValueFontSize;
        scoreValueText.text = score.ToString();
    }

    // Method to end the game
    public void EndGame()
    {
        Debug.Log("Game Over!");
        int prevHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > prevHighScore)
            PlayerPrefs.SetInt(HighScoreKey, score);
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(HighScoreKey, 0);
        Time.timeScale = 0;
    }

    // Method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
