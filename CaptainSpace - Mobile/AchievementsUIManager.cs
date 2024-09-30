using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementsUIManager : MonoBehaviour
{
    public static AchievementsUIManager Instance;
    [SerializeField] private TextMeshProUGUI trophyNameText;
    [SerializeField] private TextMeshProUGUI AchievementDescrText;
    [SerializeField] private Button turnLeftPageButton;
    [SerializeField] private Button turnRightPageButton;

    [SerializeField] private GameObject[] pagesInShop;
    private int indexOfOpenPage;

    private void Awake()
    {
        Instance = this;
    }

    public void TurnPage(int pageIndex)
    {
        int pageIndexToGo = indexOfOpenPage + pageIndex;
        if (pageIndexToGo >=0 && pageIndexToGo < pagesInShop.Length)
        {
            pagesInShop[indexOfOpenPage].SetActive(false);
            pagesInShop[pageIndexToGo].SetActive(true);
            indexOfOpenPage = pageIndexToGo;

            UpdateButtonUI();

            trophyNameText.text = "";
            AchievementDescrText.text = "Page " + (pageIndexToGo + 1);
        }
    }

    public void UpdateButtonUI()
    {
        turnLeftPageButton.interactable = indexOfOpenPage > 0;
        turnRightPageButton.interactable = pagesInShop.Length - 1 > indexOfOpenPage;
    }

    public void UpdateTrophyTexts(string trophyName, string achievementDesc)
    {
        trophyNameText.text = trophyName;
        AchievementDescrText.text = achievementDesc;
    }
}
