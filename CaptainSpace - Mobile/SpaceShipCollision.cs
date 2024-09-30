using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceShipCollision : MonoBehaviour, ISpaceJumpReceiver
{
    public static SpaceShipCollision Instance;
    private SpriteRenderer sr;

    [Header("Effects")]
    [SerializeField] private ParticleSystem emerald_explode;
    [SerializeField] private ParticleSystem rubin_explode;
    [SerializeField] private ParticleSystem alien_explode;
    [SerializeField] private ParticleSystem asteroid_explode;
    [SerializeField] private ParticleSystem satellit_explode;
    [SerializeField] private ParticleSystem spaceship_explode;

    [Header("Buffs")]
    [SerializeField] private int lives = 1;
    [SerializeField] private bool hasBounty;
    [SerializeField] private bool hasGun;
    [SerializeField] private bool hasSatellitChip;
    [SerializeField] private bool hasBigAsteroidChip;
    [SerializeField] private float chanceToKillAliens = 50;

    [Header("Money stuff")]
    [SerializeField] private TextMeshProUGUI moneyFoundText;
    [SerializeField] private int emerald_worth = 20;
    [SerializeField] private int rubin_worth = 10;
    [SerializeField] private int alien_worth = 225;
    private int moneyThisTurn = 0;

    private void Awake() 
    {
        Instance = this;
        sr = GetComponent<SpriteRenderer>();
    }

    //Each entity case, what happens when we hit them
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Entity")){

            switch (collision.GetComponent<EntityType>().entityType){

                case EntityType.EntityTypes.emerald:
                    PlayerMoney.Instance.AddMoney(emerald_worth);
                    moneyThisTurn += emerald_worth;
                    AudioManager.Instance.PlayClip("pickup");
                    PlayerPrefs.SetInt("emeraldsCollected",PlayerPrefs.GetInt("emeraldsCollected") + 1);
                    Destroy(collision.gameObject);
                    PlayExplosionParticle(emerald_explode, collision.transform.position);
                    break;

                case EntityType.EntityTypes.rubin:
                    PlayerMoney.Instance.AddMoney(rubin_worth);
                    moneyThisTurn += rubin_worth;
                    AudioManager.Instance.PlayClip("gem");
                    PlayerPrefs.SetInt("rubinsCollected",PlayerPrefs.GetInt("rubinsCollected") + 1);
                    Destroy(collision.gameObject);
                    PlayExplosionParticle(rubin_explode, collision.transform.position);
                    break;
                
                case EntityType.EntityTypes.asteroid:
                    if (hasGun && CannonController.Instance.stillHasAmmo() == true)
                    {
                        CannonController.Instance.Shoot();
                        PlayerPrefs.SetInt("astShot",PlayerPrefs.GetInt("astShot") + 1);
                        Destroy(collision.gameObject);
                        PlayExplosionParticle(asteroid_explode, collision.transform.position);
                    }
                    else
                        OnHitEnemy();
                    break;
                
                case EntityType.EntityTypes.alien:
                    float luck = Random.Range(1f,100f);
                    if (hasBounty)
                    {
                        KillAlien();
                        Destroy(collision.gameObject);
                        PlayExplosionParticle(alien_explode, collision.transform.position);
                        break; 
                    }
                    else if(luck <= chanceToKillAliens)
                    {
                        OnHitEnemy();
                        break;
                    }
                    else
                    {
                        KillAlien();
                        Destroy(collision.gameObject);
                        PlayExplosionParticle(alien_explode, collision.transform.position);
                        break; 
                    } 

                case EntityType.EntityTypes.bigAsteorid:
                    if (hasGun && CannonController.Instance.stillHasAmmo() == true && hasBigAsteroidChip)
                    {
                        CannonController.Instance.Shoot();
                        AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.big_gunner);
                        PlayerPrefs.SetInt("bigAstShot",PlayerPrefs.GetInt("bigAstShot") + 1);
                        Destroy(collision.gameObject);
                        PlayExplosionParticle(asteroid_explode, collision.transform.position);
                    }
                    else
                        OnHitEnemy();

                    break; 
                    

                case EntityType.EntityTypes.satellit:
                    if (hasGun && CannonController.Instance.stillHasAmmo() == true && hasSatellitChip)
                    {
                        CannonController.Instance.Shoot();
                        PlayerPrefs.SetInt("satelitShot",PlayerPrefs.GetInt("satelitShot") + 1);
                        Destroy(collision.gameObject);
                        PlayExplosionParticle(satellit_explode, collision.transform.position);
                    }
                    else
                        OnHitEnemy();
                        
                    break;

                case EntityType.EntityTypes.rapid_asteroid:
                    if (hasGun && CannonController.Instance.stillHasAmmo() == true)
                    {
                        CannonController.Instance.Shoot();
                        PlayerPrefs.SetInt("astShot",PlayerPrefs.GetInt("astShot") + 1);
                        Destroy(collision.gameObject);
                        PlayExplosionParticle(asteroid_explode, collision.transform.position);
                    }
                    else
                        OnHitEnemy();
                    break;                
            }
        }
    }

    private void Update() 
    {
        moneyFoundText.text = moneyThisTurn + " $";
    }

    private void PlayExplosionParticle(ParticleSystem currentExplode, Vector2 position)
    {
        if (currentExplode != null)
        {
        ParticleSystem explosionParticle = Instantiate(currentExplode, position, Quaternion.identity);
        }
    }

    private void KillAlien()
    {
        //Unlock achievement
        AchievementManager.Instance.UnlockAchievement(Achievement.AchievementTypes.first_blood);
        //Add money to balance
        PlayerMoney.Instance.AddMoney(alien_worth);
        //Stats count
        PlayerPrefs.SetInt("aliensKilled",PlayerPrefs.GetInt("aliensKilled") + 1);
        //Show the money we got from killing it
        moneyThisTurn += alien_worth;
        //Audio
        AudioManager.Instance.PlayClip("kill");
    }

    private void OnHitEnemy()
    {
        lives--;
        LifeController.Instance.LoseHeart();
        if ( lives <= 0)
        {
            SpaceshipExplosion();
            AudioManager.Instance.PlayClip("death");
            FinishGame.Instance.Finish();
        }
        else 
        {
            StartCoroutine(LostLife());
        }
    }

    private IEnumerator LostLife()
    {
        Time.timeScale = 0.5f;
        //Player ne ütközhessen enemy-vel
        gameObject.layer = 6;
        AudioManager.Instance.PlayClip("onlyHit");
        //Animáció
        int i = 0;
        while(i < 3)
        {
            i++;
            sr.enabled = false;
            SpaceshipSkin.Instance.flame.Stop();
            yield return new WaitForSeconds(0.125f);
            sr.enabled = true;
            SpaceshipSkin.Instance.flame.Play();
            yield return new WaitForSeconds(0.125f);
        }
        Time.timeScale = 1;
        gameObject.layer = 0;
    }

    private IEnumerator SpaceJumping()
    {
        Time.timeScale = 0.5f;
        int i = 0;
        while(i < 4)
        {
            i++;
            sr.enabled = false;
            yield return new WaitForSeconds(0.125f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.125f);
        }
        Time.timeScale = 1;
        gameObject.layer = 0;
    }

    public void StartScript()
    {
        lives += PlayerPrefs.GetInt(ShopItem.ShopItems.extra_shield.ToString());

        if (PlayerPrefs.GetInt(ShopItem.ShopItems.smallCannon.ToString()) == 1)
            hasGun = true;
        if (PlayerPrefs.GetInt(ShopItem.ShopItems.bigAst_shooter.ToString()) == 1)
            hasBigAsteroidChip = true;
        if (PlayerPrefs.GetInt(ShopItem.ShopItems.satellit_shooter.ToString()) == 1)
            hasSatellitChip = true;
        if (PlayerPrefs.GetInt(ShopItem.ShopItems.bounty_hunter.ToString()) == 1)
            hasBounty = true;

        emerald_worth += PlayerPrefs.GetInt(ShopItem.ShopItems.emerald_polisher.ToString()) * 5 
        + PlayerPrefs.GetInt(ShopItem.ShopItems.business_man.ToString()) * 45;

        rubin_worth += PlayerPrefs.GetInt(ShopItem.ShopItems.rubin_maker.ToString()) * 25
        + PlayerPrefs.GetInt(ShopItem.ShopItems.business_man.ToString()) * 45;

        alien_worth += PlayerPrefs.GetInt(ShopItem.ShopItems.alienInc.ToString()) * 75;
    }

    public void ActivateSpaceJump(float speedMultiplier)
    {
        gameObject.layer = 6;
    }

    public void SpaceshipExplosion()
    {
        PlayExplosionParticle(spaceship_explode, gameObject.transform.position);
        Destroy(gameObject);
    }

    public void EndSpaceJump()
    {
        StartCoroutine(SpaceJumping());
    }
}
