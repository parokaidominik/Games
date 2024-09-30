using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public static CannonController Instance;

    [SerializeField] private Image[] Ammonutions;
    [SerializeField] private GameObject ammoPanel;

    private int ammo;

    private void Awake()
    {
        Instance = this;
    }

    public bool stillHasAmmo()
    {
        if (ammo > -1)
            return true;
        else
            return false;
    }

    public void Shoot()
    {
        Debug.Log("Before shoot: "+PlayerPrefs.GetInt(ShopItem.ShopItems.smallCannonAmmo.ToString()) + " ammo");

        PlayerPrefs.SetInt(ShopItem.ShopItems.smallCannonAmmo.ToString(),ammo);

        Ammonutions[ammo] = Ammonutions[ammo].GetComponent<Image>();
        Ammonutions[ammo].color = new Color(Ammonutions[ammo].color.r, Ammonutions[ammo].color.g, Ammonutions[ammo].color.b, 0.5f);
        ammo--;

        AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.shoot_asteroid);
        
        AudioManager.Instance.PlayClip("kill");
        Debug.Log("After shoot: "+PlayerPrefs.GetInt(ShopItem.ShopItems.smallCannonAmmo.ToString()) + " ammo");
    }

    public void StartScript()
    {
        if (PlayerPrefs.GetInt(ShopItem.ShopItems.smallCannon.ToString()) == 1)
            ammoPanel.SetActive(true);

        ammo = PlayerPrefs.GetInt(ShopItem.ShopItems.smallCannonAmmo.ToString()) -1;
        if (ammo > -1)
        {
            for (int i = 0; i <= ammo; i++)
            {
                Ammonutions[i] = Ammonutions[i].GetComponent<Image>();
                Ammonutions[i].color = new Color(Ammonutions[i].color.r, Ammonutions[i].color.g, Ammonutions[i].color.b, 1);
            }
            
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Ammonutions[i] = Ammonutions[i].GetComponent<Image>();
                Ammonutions[i].color = new Color(Ammonutions[i].color.r, Ammonutions[i].color.g, Ammonutions[i].color.b, 0.5f);
            }
        }
    }
}
