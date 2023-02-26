using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        InitializeScore();
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

    // Method to Update the score and display the updated score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
