using System;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    [SerializeField] private Achievement[] trophies;
    private void Awake() 
    {
        Instance = this;
    }

    public void UnlockAchievement(Achievement.AchievementTypes achievementType)
    {
        Achievement achievementToUnlock = Array.Find(trophies, l => l._achievementType == achievementType);

        if (achievementToUnlock == null)
        {
            Debug.LogWarning("No trophy found for "+ achievementType +" achievement.");
            return;
        }

        if (!achievementToUnlock.isUnlocked)
        {
            achievementToUnlock.UnlockThisAchievement();
        }
    }

}
