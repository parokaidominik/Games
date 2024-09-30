using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;

    [SerializeField] private SkinShopItem[] allSkins;
    private string equipedSkin;

    private void Awake() 
    { 
        Instance = this;
    }

    public void EquipSkinAndUnequipOthers(string equipedSkin)
    {
        //Debug.Log("Got the "+equipedSkin+" here.");
        foreach (SkinShopItem skin in allSkins)
        {
            if(skin.SkinType.ToString() == equipedSkin)
                skin.EquipSkin();
            else
                skin.UnEquipSkins();
        }
    }
}
