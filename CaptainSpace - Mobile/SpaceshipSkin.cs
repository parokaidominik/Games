using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipSkin : MonoBehaviour
{
    public static SpaceshipSkin Instance;

    [Header("Effects")]
    [SerializeField] public ParticleSystem flame;

    [Header("Skins")]
    [SerializeField] private SpriteRenderer spaceship;
    [SerializeField] private Sprite basicSpaceship;
    [SerializeField] private Sprite spaceshipWithGuns;
    [SerializeField] private Sprite goldenSpaceship;
    [SerializeField] private Sprite goldenSpaceshipWithGuns;
    [SerializeField] private Sprite redSpaceship;
    [SerializeField] private Sprite redSpaceshipWithGuns;

    [Header("Brain")]
    [SerializeField] private bool hasGun;
    private string equipedSkin;
    private const string lastUsedSkin = "lastUsedSkin"; 
    private const string defaultSkin = "basic_spaceship";
    

    private void Awake()
    {
        Instance = this;
        spaceship = gameObject.GetComponent<SpriteRenderer>();
    }

    public void StartScript()
    {
        flame.Play();

        equipedSkin = LoadSkin();

        if (PlayerPrefs.GetInt(ShopItem.ShopItems.smallCannon.ToString()) == 1)
            hasGun = true;

        if (equipedSkin == "red_spaceship")
        {
            spaceship.sprite = redSpaceship;

            if (hasGun)
                spaceship.sprite = redSpaceshipWithGuns;
        }

        if (equipedSkin == "golden_spaceship")
        {
            AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.richAdventurer);
            spaceship.sprite = goldenSpaceship;

            if (hasGun)
                spaceship.sprite = goldenSpaceshipWithGuns;
        }

        if (equipedSkin == "basic_spaceship")
        {
            spaceship.sprite = basicSpaceship;

            if (hasGun)
                spaceship.sprite = spaceshipWithGuns;
        }

    }

    string LoadSkin()
    {
        return PlayerPrefs.HasKey(lastUsedSkin) ? PlayerPrefs.GetString(lastUsedSkin) : defaultSkin;
    }
}
