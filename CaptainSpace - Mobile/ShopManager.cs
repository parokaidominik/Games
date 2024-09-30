using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private TextMeshProUGUI moneyShopText;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        UpdateMoneyInShopUI();    
    }

    public void UpdateMoneyInShopUI()
    {
        moneyShopText.text = PlayerMoney.Instance.ReturnCurrentMoney() + " $";
    }

    public void DebugMoneyAdd()
    {
        PlayerMoney.Instance.AddMoneyAndSave(10000);
        UpdateMoneyInShopUI();
    }
}
