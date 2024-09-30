using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Difficulty : MonoBehaviour
{
    [Header("Difficulty Level")]
    [SerializeField] private TextMeshProUGUI diffText;

    [Header("Difficulty Buttons")]
    [SerializeField] private Button easyButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private Button hardButton;

    private int diff;

    public static Difficulty Instance;

    private void Awake() 
    {
        Instance = this;
        diff = 5;
    }

    private void Start()
    {
        diffText.text = "Normal";
        easyButton.interactable = true;
        normalButton.interactable = false;
        hardButton.interactable = true;
        diffText.color = new Color(0f ,219f, 255f);
    }

    public void GameDifficulty(int idx)
    {
        if (idx == 0)
        {
            diff = 3;
            diffText.text = "Easy";
            diffText.color = new Color(31f ,255f, 0f);
            Bingo.Instance.CheckDifficulty();

            easyButton.interactable = false;
            normalButton.interactable = true;
            hardButton.interactable = true;
        }
        else if (idx ==1)
        {
            diff = 5;
            diffText.text = "Normal";
            diffText.color = new Color(0f ,219f, 255f);
            Bingo.Instance.CheckDifficulty();

            easyButton.interactable = true;
            normalButton.interactable = false;
            hardButton.interactable = true;            
        }
        else if (idx == 2)
        {
            diff = 9;
            diffText.text = "Hard";
            diffText.color = Color.red;
            Bingo.Instance.CheckDifficulty();

            easyButton.interactable = true;
            normalButton.interactable = true;
            hardButton.interactable = false;            
        }
    }

    public int GetDiff()
    {
        return diff;
    }
}
