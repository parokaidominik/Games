using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, ISpaceJumpReceiver
{
    public static Movement Instance;

    private Rigidbody2D rb;
    private Camera mainCamera;

    [SerializeField] private float speed = 5;
    [SerializeField] private float xMargin = 2;
    [SerializeField] private float yMarginInput = 3;

    private bool canMove;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        Instance = this;
        Debug.Log(Screen.width);
    }

    private void FixedUpdate() {

        if(!canMove)
            return;

        int dirX = 0;
        transform.rotation = Quaternion.Euler(0,0,0);

        if(Application.isEditor){
            if(Input.GetKey(KeyCode.D)){
                dirX = 1;
                transform.rotation = Quaternion.Euler(0,0,-15);
            }
            else if(Input.GetKey(KeyCode.A)){
                dirX = -1;
                transform.rotation = Quaternion.Euler(0,0,15);
            }
        }
        else
        {
            if (Input.touches.Length > 0){
                Vector3 touchPosition = Input.touches[0].position;
                touchPosition = mainCamera.ScreenToWorldPoint(touchPosition);

                if(touchPosition.y < yMarginInput)
                {
                    if(touchPosition.x > 0)
                    {
                    //go right
                       dirX = 1;
                        transform.rotation = Quaternion.Euler(0,0,-15);
                    }
                    else 
                    {
                    //go left
                        dirX = -1;
                        transform.rotation = Quaternion.Euler(0,0,15);
                    }
                }
            }
        }

        rb.velocity = new Vector2(dirX * speed * Time.fixedDeltaTime, 0);

        float posX = transform.position.x;
        posX = Mathf.Clamp(posX,-xMargin,xMargin);
        transform.position = new Vector3(posX,transform.position.y,transform.position.z);
    }

    public void StartScript()
    {
        canMove = true;

        speed += (PlayerPrefs.GetInt(ShopItem.ShopItems.engine.ToString()) * 5) + 
        (PlayerPrefs.GetInt(ShopItem.ShopItems.wheel.ToString()) * 8) +
        (PlayerPrefs.GetInt(ShopItem.ShopItems.racer_helmet.ToString()) * 12);

    }

    public void ActivateSpaceJump(float speedMultiplier)
    {
        canMove = false;
    }

    public void EndSpaceJump()
    {
        canMove = true;
    }

}
