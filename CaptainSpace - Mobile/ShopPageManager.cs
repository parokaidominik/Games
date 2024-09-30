using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPageManager : MonoBehaviour
{
    public static ShopPageManager Instance;
    [SerializeField] private Button turnLeftPageButton;
    [SerializeField] private Button turnRightPageButton;

    [SerializeField] private GameObject[] pagesInShop;
    private int indexOfOpenPage;
    public int unlockExtraPageMetersTravel{get; private set;} = 1000;
    private const string prefExtraPagesToUnlock = "prefExtraPagesToUnlock";

    private void Awake()
    {
        Instance = this;
        UpdateButtonUI();
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
        }
    }

    public void UnlockExtraPage()
    {
        PlayerPrefs.SetInt(prefExtraPagesToUnlock, PlayerPrefs.GetInt(prefExtraPagesToUnlock) + 1);
        if (PlayerPrefs.GetInt(prefExtraPagesToUnlock) >= pagesInShop.Length)
            Debug.LogWarning("More Pages get unlocked then pages exist !");

        UpdateButtonUI();
    }

    public void UpdateButtonUI()
    {
        turnLeftPageButton.interactable = indexOfOpenPage > 0;
        turnRightPageButton.interactable = pagesInShop.Length - 1 > indexOfOpenPage;

        turnRightPageButton.interactable = PlayerPrefs.GetInt(prefExtraPagesToUnlock) >= indexOfOpenPage + 1;
    }
}
