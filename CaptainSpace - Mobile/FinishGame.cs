using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishGame : MonoBehaviour
{
   public static FinishGame Instance;
   [SerializeField] private GameObject gameOverPanel;
   [SerializeField] private GameObject gamePanel;
   [SerializeField] private GameObject unlockedPagePanel;
   [SerializeField] private TextMeshProUGUI moneyText;
   [SerializeField] private TextMeshProUGUI lostText;
   [SerializeField] private TextMeshProUGUI hihgscoreText;
   [SerializeField] private TextMeshProUGUI pagesUnlockedText;

   private int pagesBefore;

   private void Awake() 
   {
        Instance = this;
        pagesBefore = PlayerPrefs.GetInt("prefExtraPagesToUnlock");
   }

   public void BackToShop()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   public void Finish()
   {
        Time.timeScale = 0.01f;
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);

        int moneyMadeThisGame = PlayerMoney.Instance.SaveMoneyAndGetMoneyMade();
        moneyText.text = "+ "+moneyMadeThisGame+" $";

        //SAVE STATS
        PlayerPrefs.SetInt("gamesPlayed",PlayerPrefs.GetInt("gamesPlayed") + 1);
        PlayerPrefs.SetInt("moneyThisGame",moneyMadeThisGame);
        RoundStats.Instance.SaveStats();

        //ACHIEVEMENTS
        if (moneyMadeThisGame >= 10000)
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.scavenger);

        if (moneyMadeThisGame >= 100000)
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.scavenger_king);

        RoundStats.Instance.AchievementCheck();

        //Highscore
        bool isNewHighScore = TravelManager.Instance.CheckNewHighscore();
        if (isNewHighScore)
            lostText.text = "New highscore !";

        hihgscoreText.text = "Highscore : " + PlayerPrefs.GetInt("prefMeters") + " km";


        Debug.Log("pages: " + PlayerPrefs.GetInt("prefExtraPagesToUnlock") + "before : " + pagesBefore);

        //CHECK THE USER UNLOCKED ANY NEW PAGES
        if (PlayerPrefs.GetInt("prefExtraPagesToUnlock") > pagesBefore)
        {
            unlockedPagePanel.SetActive(true);
            int unlockedPagesThisRound = PlayerPrefs.GetInt("prefExtraPagesToUnlock") - pagesBefore;
            pagesUnlockedText.text = "You unlocked + " + unlockedPagesThisRound + " shop page!";
        }
        
   }
}
