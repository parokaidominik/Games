using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI topPanelText;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject infoMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject achievementMenu;
    [SerializeField] private GameObject skinShopMenu;
    [SerializeField] private GameObject statsMenu;

    [ContextMenu("OpenInfoMenu")]
    public void OpenInfoMenu()
    {
        if (infoMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text="INFO";
        infoMenu.SetActive(true);
    }

    [ContextMenu("OpenOptionsMenu")]
    public void OpenOptionsMenu()
    {
        if (optionsMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text="OPTIONS";
        optionsMenu.SetActive(true);
    }

    [ContextMenu("OpenAchievementMenu")]
    public void OpenAchievementMenu()
    {
        if (achievementMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text="ACHIEVEMENTS";
        achievementMenu.SetActive(true);
        MenuSelectManager.Instance.ChangeIsShopOpenTo(false);
    }

    private void OpenShopMenu()
    {
        MenuSelectManager.Instance.ChangeIsShopOpenTo(true);
        shopMenu.SetActive(true);
        topPanelText.text="JUNK SHOP";
    }

    [ContextMenu("OpenSkinShopMenu")]
    public void OpenSkinShopMenu()
    {
        if (skinShopMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenShopMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text="SKIN SHOP";
        skinShopMenu.SetActive(true);
    }

    [ContextMenu("OpenStatsMenu")]
    public void OpenStatsMenu()
    {
        if (statsMenu.activeInHierarchy)
        {
            CloseAllMenus();
            OpenStatsMenu();
            return;
        }

        CloseAllMenus();
        topPanelText.text="STATISTICS";
        statsMenu.SetActive(true);
    }

    [ContextMenu("CloseAllMenus")]
    private void CloseAllMenus()
    {
        shopMenu.SetActive(false);
        infoMenu.SetActive(false);
        optionsMenu.SetActive(false);
        achievementMenu.SetActive(false);
        skinShopMenu.SetActive(false);
        statsMenu.SetActive(false);
    }
}
