using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private bool timerOn = false;

    public TextMeshProUGUI timerText;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        updateTimer(timeLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is Up!");
                timeLeft = 0;
                timerOn = false;
                gameManager.EndGame();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }
}
