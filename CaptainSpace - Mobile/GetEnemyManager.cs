using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemyManager : MonoBehaviour
{
    public static GetEnemyManager Instance;

    [SerializeField] private int howManyEnemiesUnlocked;
    [SerializeField] private int howManyEnemiesUnlockedMax;

    [SerializeField] private GameObject[] entitiesPrefabs;
    [SerializeField] private float chanceToSpawnGems = 50;

    private void Awake() 
    {
        Instance = this;  
        howManyEnemiesUnlockedMax = entitiesPrefabs.Length - 3;
    }

    public GameObject GetEnemy()
    {
        float randomNumber = Random.Range(1f,100f);
        if (randomNumber <= chanceToSpawnGems)
        {
            return entitiesPrefabs[Random.Range(0, 2)];
        }

        return entitiesPrefabs[Random.Range(2, 3 + howManyEnemiesUnlocked)];
    }

    public void AddEnemy()
    {
        if (howManyEnemiesUnlocked < howManyEnemiesUnlockedMax)
            howManyEnemiesUnlocked++;
    }

    public void StartScript()
    {
        chanceToSpawnGems += (float)PlayerPrefs.GetInt(ShopItem.ShopItems.luckInc.ToString()) / 4 
        + (float)PlayerPrefs.GetInt(ShopItem.ShopItems.luckInc.ToString()) / 4;
    }
}
