﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script controlling player's interaction with propellor controls lever
/// </summary>
public class PropellorControls : MonoBehaviour
{
    #region Fields

    // sprite swapping support variables
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite leverUpSprite;
    [SerializeField] Sprite leverLeftSprite;
    [SerializeField] Sprite leverRightSprite;

    // input support variables
    [SerializeField] PartsManager partsManager;
    bool playerIsColliding = false;
    bool isOnRight = true;
    int propellorInputAxis = 0;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // retrieve reference to object's sprite renderer
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // retrieve interaction input
        float interactInput = Input.GetAxisRaw("Interact");

        // if player is colliding with controls, interacts with them, and propellors are not disabled
        if (playerIsColliding && interactInput != 0 && partsManager.GetPartFunctionality(SubmarineParts.propellors))
        {
            // adjust propellor input axis and swap sprite accordingly
            if (isOnRight)
            {
                propellorInputAxis = 1;
                mySpriteRenderer.sprite = leverRightSprite;
            }
            else
            {
                propellorInputAxis = -1;
                mySpriteRenderer.sprite = leverLeftSprite;
            }
        }
        // otherwise, lock input axis to 0 and set lever to up-position
        else
        {
            propellorInputAxis = 0;
            mySpriteRenderer.sprite = leverUpSprite;
        }
    }

    // Called on every frame controls are in collision with another object
    void OnTriggerStay2D(Collider2D collision)
    {
        // set collision flag to true
        playerIsColliding = true;

        // determine what side of controls player is on
        isOnRight = true;
        if (collision.gameObject.transform.position.x < transform.position.x)
            isOnRight = false;
    }

    // Called on first frame controls exit collision
    void OnTriggerExit2D(Collider2D collision)
    {
        // set collision flag to false
        playerIsColliding = false;
    }

    #endregion

}
