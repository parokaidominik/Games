using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectManager : MonoBehaviour
{
    public static MenuSelectManager Instance;
    private bool isShopOpen = true;

    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private float marginForSwipe = 40;
    private Vector3 startingFingerPosition;

    private void Awake() 
    {
        Instance = this;
    }

    // Checking if the user swipped in Shop or Achievements Menu
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startingFingerPosition = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (Input.touches[0].position.x > startingFingerPosition.x + marginForSwipe)
                {
                    //Swipe to right
                    if (leftButton.interactable)
                        OnMenuButtonsPress(-1);
                }
                else if (Input.touches[0].position.x < startingFingerPosition.x - marginForSwipe)
                {
                    //Swipe to left
                    if (rightButton.interactable)
                        OnMenuButtonsPress(1);
                }
            }
        }
    }

    public void OnMenuButtonsPress(int direction)
    {
        if (isShopOpen)
        {
            ShopPageManager.Instance.TurnPage(direction);
        }
        else
        {
            AchievementsUIManager.Instance.TurnPage(direction);
        }
    }

    public void ChangeIsShopOpenTo(bool value)
    {
        isShopOpen = value;

        if (isShopOpen)
        {
            ShopPageManager.Instance.UpdateButtonUI();
        }
        else
        {
            AchievementsUIManager.Instance.UpdateButtonUI();
        }
    }
}
