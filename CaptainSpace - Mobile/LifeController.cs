using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public static LifeController Instance;

    [SerializeField] private Image[] hearts;

    private int extraHearts = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void LoseHeart()
    {
        hearts[extraHearts] = hearts[extraHearts].GetComponent<Image>();
        hearts[extraHearts].color = new Color(hearts[extraHearts].color.r, hearts[extraHearts].color.g, hearts[extraHearts].color.b, 0.5f);
        extraHearts--;
    }

    public void StartScript()
    {
        extraHearts = PlayerPrefs.GetInt(ShopItem.ShopItems.extra_shield.ToString());
        if (extraHearts > 0)
        {
            for (int i = 1; i < extraHearts+1; i++)
            {
                hearts[i] = hearts[i].GetComponent<Image>();
                hearts[i].color = new Color(hearts[i].color.r, hearts[i].color.g, hearts[i].color.b, 1);
            }
        }
    }
}