using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ISpaceJumpReceiver
{
    public static SpawnManager Instance;
    [SerializeField] private bool canSpawn;
    [SerializeField] private Vector3 spawnPosition;

    [SerializeField] private float xMargin = 2;
    [SerializeField] private float spawnTimer;

    [Header("Scaling")]
    [SerializeField] private float scalingMultiplier = 1.0001f;
    [SerializeField] private float scaledSpawnTimerMax = 0.5f;
    [SerializeField] private float spawnTimerMax = 3f;
    [SerializeField] private float scaledEntitiesSpeed = 8f;
    [SerializeField] private float entitiesSpeed = 5;
    private bool maxVelocityReached;

    private void Awake() 
    {
        Instance = this;
    }

    private void Update() 
    {
        if(!canSpawn)
            return;

        IncreaseSpeed();
        TrySpawn();
    }

    public void StartScript()
    {
        canSpawn = true;
        spawnTimer = spawnTimerMax;
    }

    private void TrySpawn()
    {

        if(spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            spawnTimer = spawnTimerMax;
            SpawnEntity();
        }
    }

    private void SpawnEntity()
    {
       GameObject entityToSpawn = GetEnemyManager.Instance.GetEnemy();
       spawnPosition.x = Random.Range(-xMargin,xMargin);

       GameObject spawnedEntity = Instantiate(entityToSpawn, spawnPosition, Quaternion.identity);
       spawnedEntity.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -entitiesSpeed);
       spawnedEntity.GetComponent<EntityType>().StartEntity();
    }

    private void IncreaseSpeed()
    {
        //Check if we reached the limit speed in every 5 frame
        if(maxVelocityReached || Time.frameCount % 5 != 0 || Time.timeScale == 0)
            return;

        //Reduce the spawn time
        if (spawnTimerMax > scaledSpawnTimerMax)
            spawnTimerMax /= scalingMultiplier;
        else
            spawnTimerMax = scaledSpawnTimerMax;

        //Increase the entities speed
        if (entitiesSpeed < scaledEntitiesSpeed)
            entitiesSpeed *= scalingMultiplier;
        else
            entitiesSpeed = scaledEntitiesSpeed;

        //If we reached both max, return
        if (spawnTimerMax == scaledSpawnTimerMax && entitiesSpeed == scaledEntitiesSpeed)
            maxVelocityReached = true;

        //Give speed to entities
        GameObject[] entitiesInGame = GameObject.FindGameObjectsWithTag("Entity");
        foreach (GameObject e in entitiesInGame)
        {
            Rigidbody2D entityRb = e.GetComponent<Rigidbody2D>();
            entityRb.velocity = new Vector2(entityRb.velocity.x, -entitiesSpeed);
        }
    }

    public void ActivateSpaceJump(float speedMultiplier)
    {
        maxVelocityReached = true;
        spawnTimer = scaledSpawnTimerMax / speedMultiplier;
        spawnTimer = scaledSpawnTimerMax;
        entitiesSpeed = scaledEntitiesSpeed * speedMultiplier;
    }

    public void EndSpaceJump()
    {
        spawnTimerMax = scaledSpawnTimerMax;
        entitiesSpeed = scaledEntitiesSpeed;
    }
}
