using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    [Header("Values")]
    public ShopItems ItemType;
    public enum ShopItems { engine, emerald_polisher, 
    rocket_engine, rubin_maker, wheel, better_fuel, alienInc, 
    bounty_hunter, extra_shield, luckInc, smallCannon, smallCannonAmmo, 
    golden_spaceship, space_jump, business_man, satellit_shooter, bigAst_shooter,
    red_spaceship, racer_helmet, art_luck, admiral}

    [SerializeField] private string itemName = "Engine";
    [SerializeField] private int itemPrice = 300;
    [SerializeField] private int itemLevel;
    [SerializeField] private int itemLevelMax = 5;
    [SerializeField] private int priceUp = 50;
    private int itemPriceGrow;
    
    private void Awake() 
    {
        UpdateItemUI();
    }

    public void BuyItem()
    {
        //BUY ITEM IF WE HAVE ENOUGH MONEY AND ITEM IS NOT MAX
        if (itemLevel < itemLevelMax && PlayerMoney.Instance.ReturnCurrentMoney() >= (itemPrice + itemPriceGrow))
        {
            AudioManager.Instance.PlayClip("buy");
            
            itemLevel++;
            PlayerPrefs.SetInt(ItemType.ToString(), itemLevel);

            PlayerMoney.Instance.AddMoneyAndSave(-(itemPrice + itemPriceGrow));
            CheckSpentMoney((itemPrice + itemPriceGrow));

            UpdateItemUI();
            ShopManager.Instance.UpdateMoneyInShopUI();
            LockedShopItem.UpdateAllLockedItems();
        }

        //If lvl is not Max but we dont have enough money
        else if (itemLevel < itemLevelMax && PlayerMoney.Instance.ReturnCurrentMoney() < (itemPrice + itemPriceGrow))
        {
           AudioManager.Instance.PlayClip("error"); 
        }
    }

    private void UpdateItemUI()
    {
        itemLevel = PlayerPrefs.GetInt(ItemType.ToString());

        itemPriceGrow = itemLevel * priceUp;
        itemNameText.text = "Lvl. "+ itemLevel + " " + itemName;
        itemPriceText.text = itemPrice + itemPriceGrow +"$";

        if(itemLevel == itemLevelMax)
        {
            itemNameText.text = "Max Lvl. : " + itemName;
            itemPriceText.text = "MAX";
        }
    }

    //ACHIEVEMENT
    private void CheckSpentMoney(int spentMoney)
    {
        PlayerPrefs.SetInt("spentMoney",PlayerPrefs.GetInt("spentMoney") + spentMoney);

        if (PlayerPrefs.GetInt("spentMoney") >= 100000)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.spend_100k);
        }
        if (PlayerPrefs.GetInt("spentMoney") >= 750000)
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.spend_750k);
        }
    }
}
