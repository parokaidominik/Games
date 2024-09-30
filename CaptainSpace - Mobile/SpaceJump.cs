using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJump : MonoBehaviour
{
    public static SpaceJump Instance;

    [SerializeField] private float jumpDuration = 1;
    [SerializeField] private float speedMultiplier = 2;
    [SerializeField] private MoveBackground[] backgrounds; 
    [SerializeField] private ParticleSystem spaceJump;

    private void Awake() 
    {
        Instance = this;    
    }

    public void StartScript()
    {
        if (PlayerPrefs.GetInt(ShopItem.ShopItems.space_jump.ToString()) >= 1)
        {
            TravelManager.Instance.ActivateSpaceJump(speedMultiplier);
            SpawnManager.Instance.ActivateSpaceJump(speedMultiplier);
            Movement.Instance.ActivateSpaceJump(speedMultiplier);
            SpaceShipCollision.Instance.ActivateSpaceJump(speedMultiplier);
            foreach (MoveBackground bg in backgrounds)
            {
                bg.ActivateSpaceJump(speedMultiplier);
            }
            spaceJump.Play();
            AudioManager.Instance.PlayClip("spaceJump");

            Invoke("TurnOffSpaceJump", jumpDuration);
        }
    }

    private void TurnOffSpaceJump()
    {
        TravelManager.Instance.EndSpaceJump();
        SpawnManager.Instance.EndSpaceJump();
        Movement.Instance.EndSpaceJump();
        SpaceShipCollision.Instance.EndSpaceJump();
        foreach (MoveBackground bg in backgrounds)
        {
            bg.EndSpaceJump();
        }
    }
}

public interface ISpaceJumpReceiver
{
    void ActivateSpaceJump(float speedMultiplier);

    void EndSpaceJump();
}
