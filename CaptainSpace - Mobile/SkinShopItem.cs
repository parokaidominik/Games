using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinShopItem : MonoBehaviour
{
    public static SkinShopItem Instance;

    [Header("Buttons")]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private Button equipButtonButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI equipButtonText;

    [Header("Values")]
    public SkinShopItems SkinType;
    public enum SkinShopItems {golden_spaceship, red_spaceship, basic_spaceship}

    [SerializeField] private string itemName = "Spaceship";
    [SerializeField] private int itemPrice = 300;
    [SerializeField] private int itemLevel;
    [SerializeField] private int itemLevelMax = 5;

    [Header("Brain")]
    [SerializeField] public bool isFree;
    private static bool playedBefore;
    public static string equipedSkin;
    private const string lastUsedSkin = "lastUsedSkin"; 
    private const string defaultSkin = "basic_spaceship";
    
    private void Awake() 
    {
        Instance = this;

        if (isFree)
            PlayerPrefs.SetInt(SkinType.ToString(), 1);

        equipedSkin = LoadSkin();
        if (SkinType.ToString() == equipedSkin)
            EquipSkin();

        Debug.Log("This is what i have in pref : "+PlayerPrefs.GetString(lastUsedSkin));

        UpdateItemUI(); 
    }

    public void BuyItem()
    {

        //IF WE HAVE ENOUGH MONEY, BUY SKIN
        if (itemLevel < itemLevelMax && PlayerMoney.Instance.ReturnCurrentMoney() >= itemPrice)
        {
            AudioManager.Instance.PlayClip("buy");
            
            itemLevel++;
            PlayerPrefs.SetInt(SkinType.ToString(), itemLevel);

            PlayerMoney.Instance.AddMoneyAndSave(-(itemPrice));

            CheckSpentMoney(itemPrice);

            UpdateItemUI();
            ShopManager.Instance.UpdateMoneyInShopUI();
        }

        //IF WE DONT HAVE ENOUGH MONEY PLAY ERROR
        else if (itemLevel < itemLevelMax && PlayerMoney.Instance.ReturnCurrentMoney() < itemPrice)
        {
           AudioManager.Instance.PlayClip("error"); 
        }
    }

    private void UpdateItemUI()
    {
        itemLevel = PlayerPrefs.GetInt(SkinType.ToString());

        itemNameText.text = itemName;
        itemPriceText.text = itemPrice +"$";

        if(itemLevel == itemLevelMax || isFree)
        {
            buyButton.SetActive(false);
            equipButton.SetActive(true);
        }
    }

    public void EquipSkin()
    {
        equipedSkin = SkinType.ToString();
        equipButtonButton.interactable = false;
        equipButtonText.text = "EQUIPPED";
        PlayerPrefs.SetString(lastUsedSkin, equipedSkin);
    }

    public void UnEquipSkins()
    {
        equipButtonText.text = "EQUIP";
        equipButtonButton.interactable = true;
    }

    public string LoadSkin()
    {
        return PlayerPrefs.HasKey(lastUsedSkin) ? PlayerPrefs.GetString(lastUsedSkin) : defaultSkin;
    }

    
    private void CheckSpentMoney(int spentMoney)
    {
        PlayerPrefs.SetInt("spentMoney",PlayerPrefs.GetInt("spentMoney") + spentMoney);

        Debug.Log(PlayerPrefs.GetInt("spentMoney").ToString());
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
