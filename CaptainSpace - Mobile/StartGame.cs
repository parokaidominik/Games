using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private MoveBackground[] movingBackgrounds;

    public void StartGameplay()
    {
        SpawnManager.Instance.StartScript();
        TravelManager.Instance.StartScript();
        Movement.Instance.StartScript();
        LifeController.Instance.StartScript();
        CannonController.Instance.StartScript();
        GetEnemyManager.Instance.StartScript();
        SpaceShipCollision.Instance.StartScript();
        SpaceshipSkin.Instance.StartScript();
        SpaceJump.Instance.StartScript();
        RoundStats.Instance.StartScript();

        foreach (MoveBackground bg in movingBackgrounds)
        {
            bg.StartScript();
        }
        
        MenuSelectManager.Instance.enabled = false;
        enabled = false;
    }
}
