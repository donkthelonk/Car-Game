using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private bool timerOn = false;
    private bool isFlashing = false;

    public TextMeshProUGUI timerValueText;
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
                if (!isFlashing)
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

    public void AddTime(float amount)
    {
        timeLeft += amount;
        StartCoroutine(FlashTimerText());
    }

    void updateTimer(float currentTime)
    {
        timerValueText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    IEnumerator FlashTimerText()
    {
        isFlashing = true;
        timerValueText.text = "<color=yellow><size=110%>" + Mathf.CeilToInt(timeLeft) + "</size></color>";
        yield return new WaitForSeconds(0.3f);
        isFlashing = false;
        timerValueText.text = Mathf.CeilToInt(timeLeft).ToString();
    }
}
