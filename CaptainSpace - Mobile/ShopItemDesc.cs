using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemDesc : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private GameObject itemDescTextButton;
    [SerializeField] private GameObject itemImageButton;

    [SerializeField] private string description;


    public void ShowDescription()
    {
        itemDesc.text = description;

        itemImageButton.SetActive(false);
        itemDescTextButton.SetActive(true);
    }

    public void CloseDescription()
    {
        itemImageButton.SetActive(true);
        itemDescTextButton.SetActive(false);
    }
}
