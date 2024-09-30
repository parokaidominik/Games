using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundStats : MonoBehaviour
{
    [Header("CURRENT ROUNDS STATS")]
    [SerializeField] private TextMeshProUGUI emeraldsText;
    [SerializeField] private TextMeshProUGUI rubinsText;
    [SerializeField] private TextMeshProUGUI aliensKilledText;
    [SerializeField] private TextMeshProUGUI asteroidsShotText;
    [SerializeField] private TextMeshProUGUI bigAsteroidsShotText;
    [SerializeField] private TextMeshProUGUI satelitsShotText;
    [SerializeField] private TextMeshProUGUI difBetweenHighscore;
    [SerializeField] private TextMeshProUGUI moneyMade;

    [Header("ALL TIME STATS")]
    [SerializeField] private TextMeshProUGUI emeText;
    [SerializeField] private TextMeshProUGUI rubText;
    [SerializeField] private TextMeshProUGUI aliTExt;
    [SerializeField] private TextMeshProUGUI astText;
    [SerializeField] private TextMeshProUGUI bigText;
    [SerializeField] private TextMeshProUGUI satText;
    [SerializeField] private TextMeshProUGUI allKmText;
    [SerializeField] private TextMeshProUGUI gamesText;
    [SerializeField] private TextMeshProUGUI allMoneyText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public static RoundStats Instance;
    private float emeAvg;
    private float rubAvg;
    private float alienAvg;
    private int kmAvg;
    private int moneyAvg;
    private int currentHS;

    private void Awake()
    {
        Instance = this;
    }

    //Set the round stats
    public void SetStats()
    {
        emeraldsText.text = "Emeralds collected: " + PlayerPrefs.GetInt("emeraldsCollected");
        rubinsText.text = "Rubins collected: " + PlayerPrefs.GetInt("rubinsCollected");
        aliensKilledText.text = "Aliens killed: " + PlayerPrefs.GetInt("aliensKilled");
        asteroidsShotText.text = "Asteroids shot: " + PlayerPrefs.GetInt("astShot");
        bigAsteroidsShotText.text = "Big Asteroids shot: " + PlayerPrefs.GetInt("bigAstShot");
        satelitsShotText.text = "Satelits shot: " + PlayerPrefs.GetInt("satelitShot");
        moneyMade.text = "Money made: " + PlayerPrefs.GetInt("moneyThisGame") +" $";

        if (currentHS < (int)TravelManager.Instance.metersTravelled)
        {
            int diffKm = (int)TravelManager.Instance.metersTravelled - currentHS;
            difBetweenHighscore.text = "You broke your record with: "+ (diffKm) +" km";
        }

        else if ((int)TravelManager.Instance.metersTravelled < currentHS)
        {
            int diffKm = PlayerPrefs.GetInt("prefMeters") - (int)TravelManager.Instance.metersTravelled;
            difBetweenHighscore.text = "Km needed to break your  record: "+ (diffKm + 1) +" km";
        }
    }

    //Save the round stats to all time statics
    public void SaveStats()
    {
        PlayerPrefs.SetInt("emeStat",PlayerPrefs.GetInt("emeStat") + PlayerPrefs.GetInt("emeraldsCollected"));
        PlayerPrefs.SetInt("rubStat",PlayerPrefs.GetInt("rubStat") + PlayerPrefs.GetInt("rubinsCollected"));
        PlayerPrefs.SetInt("aliStat",PlayerPrefs.GetInt("aliStat") + PlayerPrefs.GetInt("aliensKilled"));
        PlayerPrefs.SetInt("astStat",PlayerPrefs.GetInt("astStat") + PlayerPrefs.GetInt("astShot"));
        PlayerPrefs.SetInt("bigStat",PlayerPrefs.GetInt("bigStat") + PlayerPrefs.GetInt("bigAstShot"));
        PlayerPrefs.SetInt("satStat",PlayerPrefs.GetInt("satStat") + PlayerPrefs.GetInt("satelitShot"));
        PlayerPrefs.SetInt("allKm",PlayerPrefs.GetInt("allKm") + (int)TravelManager.Instance.metersTravelled);
        PlayerPrefs.SetInt("moneyMadeAll",PlayerPrefs.GetInt("moneyMadeAll") + PlayerPrefs.GetInt("moneyThisGame"));
    }

    //Set the all time statics
    public void SetStatsForPlayer()
    {
        SetAvgStats();
        emeText.text = "Emeralds collected: " + PlayerPrefs.GetInt("emeStat") + " (" + emeAvg.ToString("F1") + ")";
        rubText.text = "Rubins collected: " + PlayerPrefs.GetInt("rubStat") + " (" + rubAvg.ToString("F1") + ")";
        aliTExt.text = "Aliens killed: " + PlayerPrefs.GetInt("aliStat") + " (" + alienAvg.ToString("F1") + ")";
        astText.text = "Asteroids shot: " + PlayerPrefs.GetInt("astStat");
        bigText.text = "Big Asteroids shot: " + PlayerPrefs.GetInt("bigStat");
        satText.text = "Satelits shot: " + PlayerPrefs.GetInt("satStat");
        allKmText.text = "Kms travelled: " + PlayerPrefs.GetInt("allKm") + " km" + " (" + kmAvg + " km)";
        gamesText.text = "Games played: " + PlayerPrefs.GetInt("gamesPlayed");
        allMoneyText.text = "Money made: " + PlayerPrefs.GetInt("moneyMadeAll") + " $" + " (" + moneyAvg + " $)"; 
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("prefMeters") + " km";
    }

    // Calculate avgs
    private void SetAvgStats()
    {
        emeAvg = (float)PlayerPrefs.GetInt("emeStat") / (float)PlayerPrefs.GetInt("gamesPlayed");
        rubAvg = (float)PlayerPrefs.GetInt("rubStat") / (float)PlayerPrefs.GetInt("gamesPlayed");
        alienAvg = (float)PlayerPrefs.GetInt("aliStat") / (float)PlayerPrefs.GetInt("gamesPlayed");
        kmAvg = PlayerPrefs.GetInt("allKm") / PlayerPrefs.GetInt("gamesPlayed");
        moneyAvg = PlayerPrefs.GetInt("moneyMadeAll") / PlayerPrefs.GetInt("gamesPlayed");
    }

    public void StartScript()
    {
        currentHS = PlayerPrefs.GetInt("prefMeters");
        //Reset round stats
        PlayerPrefs.SetInt("emeraldsCollected", 0);
        PlayerPrefs.SetInt("rubinsCollected", 0);
        PlayerPrefs.SetInt("aliensKilled", 0);
        PlayerPrefs.SetInt("astShot", 0);
        PlayerPrefs.SetInt("bigAstShot", 0);
        PlayerPrefs.SetInt("satelitShot", 0);
        PlayerPrefs.SetInt("moneyThisGame", 0);
    }

    //ACHIEVEMENTS
    public void AchievementCheck()
    {
        if ((PlayerPrefs.GetInt("emeStat") + PlayerPrefs.GetInt("rubStat")) >= 5000)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.gem_collector);
        }
        if (PlayerPrefs.GetInt("gamesPlayed") >= 100)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.veteran);
        }
        if (PlayerPrefs.GetInt("satStat") >= 100)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.satelit_100);
        }
        if (PlayerPrefs.GetInt("aliStat") >= 100)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.alien_100);
        }
        if (PlayerPrefs.GetInt("aliStat") >= 1000)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.alien_1000);
        }
        if ((PlayerPrefs.GetInt("astStat") + PlayerPrefs.GetInt("bigStat")) >= 100)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.asteroid_100);
        }
        
    }
}
