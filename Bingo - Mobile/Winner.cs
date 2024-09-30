using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Winner : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gamePanel;

    private float elapsedTime = 0f;
    private int secondsPassed = 0;
    private int minutesPassed = 0;
    private int hoursPassed = 0;
    private bool isGameStarted;

    public static Winner Instance;


    private void Awake() 
    {
        Instance = this;
    }

    void Update()
    {
        if (isGameStarted)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f)
            {
                secondsPassed++;
                elapsedTime -= 1f;
            }
            if (secondsPassed >= 60)
            {
                minutesPassed++;
                secondsPassed = 0;
            }
            if (minutesPassed >= 60)
            {
                hoursPassed++;
                minutesPassed = 0;
            }
        }
        
    }

    public void Win()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
        timeText.text = "Time took to get BINGO : "+hoursPassed+"h "+minutesPassed+"m "+secondsPassed+"s !";
    }

    public void GameStart()
    {
        isGameStarted = true;
    }

    public void FinishGame()
    {
        isGameStarted = false;
    }
}
