using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseText;
    [SerializeField] private Image pauseButtonImg;

    [SerializeField] private Sprite pauseButtonSprite;
    [SerializeField] private Sprite playButtonSprite;

    [SerializeField] private GameObject SelfExplodButton;

    private bool isGamePaused;
    private bool canPause = true;
    private float timeBeforePause;
    
    public void TogglePause()
    {
        if(!canPause)
            return;

        isGamePaused = !isGamePaused;

        if(isGamePaused)
        {
            pauseText.SetActive(true);
            SelfExplodButton.SetActive(true);
            pauseButtonImg.sprite = playButtonSprite;

            timeBeforePause = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            pauseText.SetActive(false);
            SelfExplodButton.SetActive(false);
            pauseButtonImg.sprite = pauseButtonSprite;

            Time.timeScale = timeBeforePause;
        }
    }

    public void ChangePauseTo(bool changeItTo)
    {
        canPause = changeItTo;
    }
}
