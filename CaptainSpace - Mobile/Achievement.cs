using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    private Image img;
    public enum AchievementTypes {first_100, first_blood, scavenger_king, 
    space_adventurer, explorer, shoot_asteroid, richAdventurer, scavenger, big_gunner,
    spend_100k, spend_750k, kingOfSpace, alien_100, alien_1000, asteroid_100, satelit_100, gem_collector,
    veteran}
    [SerializeField] private string trophyName;
    [SerializeField] private string achievementDesc;
    [SerializeField] private AchievementTypes achievementType;
    public AchievementTypes _achievementType {get {return achievementType;} }
    public bool isUnlocked { get; private set; }

    private void Awake() 
    {
        img = GetComponent<Image>(); 
        CheckIfAchievementUnlocked();   
    }

    public void CheckIfAchievementUnlocked()
    {
        if (PlayerPrefs.GetInt(achievementType.ToString()) == 0)
        {
            img.color = Color.black;
        }
        else
        {
            img.color = Color.white;
            isUnlocked = true;
        }
    }

    public void UnlockThisAchievement()
    {
        PlayerPrefs.SetInt(achievementType.ToString(), 1);
        Awake();
    }

    public void OnTouchTrophy()
    {
        AchievementsUIManager.Instance.UpdateTrophyTexts(trophyName,achievementDesc);
    }
}
