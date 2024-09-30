using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour, ISpaceJumpReceiver
{  
    [SerializeField] private Vector3 positionToGoBack;
    [SerializeField] private Vector3 destination;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float scalingMultiplier;
    private bool isSpeedingUp;

    private void Update() 
    {
        if ( Time.timeScale == 0)
            return;

        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.timeScale);    

        if (transform.position == destination)
            transform.position = positionToGoBack;

        if(isSpeedingUp)
        {
            speed *=scalingMultiplier;
            if (speed >= maxSpeed)
            {
                speed = maxSpeed;
                isSpeedingUp = false;   
            }
        }
    }

    public void StartScript()
    {
        isSpeedingUp = true;
    }

    public void ActivateSpaceJump(float speedMultiplier)
    {
        isSpeedingUp = false;
        speed = maxSpeed * speedMultiplier;
    }

    public void EndSpaceJump()
    {
        speed = maxSpeed;
    }
}
