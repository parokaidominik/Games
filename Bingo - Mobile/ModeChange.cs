using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModeChange : MonoBehaviour
{
    [Header("Edition")]
    [SerializeField] private TextMeshProUGUI modeText;
    [SerializeField] private Button cityButton;
    [SerializeField] private Button villageButton;
    [SerializeField] private Button carsButton;

    public bool isCity = true;
    public bool isCars = false;
    public static ModeChange Instance;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        modeText.text = "CITY EDITION";
        cityButton.interactable = false;
        villageButton.interactable = true;
        carsButton.interactable = true;
    }

    public void ChangePlayMode(int idx)
    {
        if (idx == 0)
        {
            isCity = true;
            isCars = false;
            modeText.text = "CITY EDITION";
            cityButton.interactable = false;
            villageButton.interactable = true;
            carsButton.interactable = true;
        }
        if (idx == 1)
        {
            isCity = false;
            isCars = false;
            modeText.text = "VILLAGE EDITION";
            cityButton.interactable = true;
            villageButton.interactable = false;
            carsButton.interactable = true;
        }
        if (idx == 2)
        {
            isCity = false;
            isCars = true;
            modeText.text = "CARS EDITION";
            cityButton.interactable = true;
            villageButton.interactable = true;
            carsButton.interactable = false;
        }
    }

}
