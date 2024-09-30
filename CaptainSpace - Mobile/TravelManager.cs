using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TravelManager : MonoBehaviour, ISpaceJumpReceiver
{
    public static TravelManager Instance;
    [SerializeField] private TextMeshProUGUI travelledText;

    public float metersTravelled {get; private set;}
    public const string prefMeters = "prefMeters";
    private bool isTraveling;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float forwardSpeedMax;
    [SerializeField] private float forwardSpeedMultiplier;
    private bool isSpeedingUp;

    private void Awake() 
    {
        Instance = this;
    }
    
    void Update()
    {
        if(!isTraveling)
            return;

        metersTravelled += Time.deltaTime * forwardSpeed;
        travelledText.text = (int)metersTravelled + " km";

        if (isSpeedingUp)
            SpeedUp();
        
    }

    public void StartScript()
    {
        isTraveling = true;
        isSpeedingUp = true;

        int rocket_engineLevel = PlayerPrefs.GetInt(ShopItem.ShopItems.rocket_engine.ToString());
        int better_fuelLevel = PlayerPrefs.GetInt(ShopItem.ShopItems.better_fuel.ToString());
        int admiralLevel = PlayerPrefs.GetInt(ShopItem.ShopItems.admiral.ToString());

        if(rocket_engineLevel > 0)
        {
            forwardSpeed += (rocket_engineLevel + better_fuelLevel + admiralLevel) * 0.5f;
            forwardSpeedMax += (float)(rocket_engineLevel + better_fuelLevel + admiralLevel) / 2;
        }
    }

    private void SpeedUp()
    {
        if (Time.timeScale == 0)
            return;

        forwardSpeed *= forwardSpeedMultiplier;

        if(forwardSpeed >= forwardSpeedMax)
        {
            forwardSpeed = forwardSpeedMax;
            isSpeedingUp = false;
        }
    }

    public bool CheckNewHighscore()
    {
        //ACHIEVEMENTS
        if ((int)metersTravelled >= 100)
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.first_100);

        if ((int)metersTravelled >= 1000)
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.space_adventurer);

        if ((int)metersTravelled >= 5000)
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.explorer); 

         if ((int)metersTravelled >= 10000)
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.kingOfSpace);          
        

        //PAGE UNLOCKS
        if ((int)metersTravelled >= 800 && PlayerPrefs.GetInt(prefMeters) < 800)
            ShopPageManager.Instance.UnlockExtraPage();
        
        if ((int)metersTravelled >= 1600 && PlayerPrefs.GetInt(prefMeters) < 1600)
            ShopPageManager.Instance.UnlockExtraPage();

        if ((int)metersTravelled >= 2400 && PlayerPrefs.GetInt(prefMeters) < 2400)
            ShopPageManager.Instance.UnlockExtraPage();

        if ((int)metersTravelled >= 5000 && PlayerPrefs.GetInt(prefMeters) < 5000)
            ShopPageManager.Instance.UnlockExtraPage();

        //HIGHSCORE CHECK
        if ((int)metersTravelled > PlayerPrefs.GetInt(prefMeters))
        {
            PlayerPrefs.SetInt(prefMeters,(int)metersTravelled);
            Debug.Log("New highscore : "+ (int)metersTravelled);
            return true;
        }
        else
        {
            Debug.Log("No new highscore");
            return false;
        }
    }

    public void ActivateSpaceJump(float speedMultiplier)
    {
        isSpeedingUp = false;
        forwardSpeed = forwardSpeedMax * speedMultiplier;
    }

    public void EndSpaceJump()
    {
        forwardSpeed = forwardSpeedMax;
    }
}
