using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedShopItem : MonoBehaviour
{
    [SerializeField] private Button buyButton = null;

    [SerializeField] private ShopItem.ShopItems itemType;
    [SerializeField] private int itemLevel = 1;

    private void OnEnable() 
    {
        UpdateButton();    
    }
    private void UpdateButton()
    {
        buyButton.interactable = isItemUnlocked();
    }

    private bool isItemUnlocked()
    {
        return PlayerPrefs.GetInt(itemType.ToString()) >= itemLevel;
    }

    public static void UpdateAllLockedItems()
    {
        LockedShopItem[] lockedItemsInScene = FindObjectsOfType<LockedShopItem>();

        foreach (LockedShopItem i in lockedItemsInScene)
        {
            i.UpdateButton();
        }
    }
}
