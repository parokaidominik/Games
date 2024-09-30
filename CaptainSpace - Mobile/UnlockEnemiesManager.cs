using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockEnemiesManager : MonoBehaviour
{
    [SerializeField] private int metersToBeAtForIncDiff = 200;
    private int prevMetersReached;

    private void Update() 
    {
        int currentMetersTravelled = (int)TravelManager.Instance.metersTravelled; 
        if ( currentMetersTravelled % metersToBeAtForIncDiff == 0 
            && currentMetersTravelled != prevMetersReached)
            {
                prevMetersReached = currentMetersTravelled;
                GetEnemyManager.Instance.AddEnemy();
            }
            
    }
}
