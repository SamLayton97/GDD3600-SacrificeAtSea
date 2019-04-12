using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script controlling player's interaction with propellor controls lever
/// </summary>
public class SubmarineControls : MonoBehaviour
{
    #region Fields

    // sprite swapping support variables
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite leverUpSprite;
    [SerializeField] Sprite leverLeftSprite;
    [SerializeField] Sprite leverRightSprite;

    // input support variables
    [SerializeField] PartsManager partsManager;
    [SerializeField] SubmarineParts myCorrespondingPart = SubmarineParts.ballastTanks;
    bool playerIsColliding = false;
    bool isOnRight = true;
    int inputAxis = 0;

    // event support
    UpdateMovementEvent updateMovementEvent;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // retrieve reference to object's sprite renderer
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // add self as invoker of update movement event
        updateMovementEvent = new UpdateMovementEvent();
        EventManager.AddUpdateMovementInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        // retrieve interaction input
        float interactInput = Input.GetAxisRaw("Interact");

        // if player is colliding with controls, interacts with them, and corresponding parts are not disabled
        if (playerIsColliding && interactInput != 0 && partsManager.GetPartFunctionality(myCorrespondingPart))
        {
            // adjust input axis and swap sprite accordingly
            if (isOnRight)
            {
                inputAxis = 1;
                mySpriteRenderer.sprite = leverRightSprite;
            }
            else
            {
                inputAxis = -1;
                mySpriteRenderer.sprite = leverLeftSprite;
            }
        }
        // otherwise, lock input axis to 0 and set lever to up-position
        else
        {
            inputAxis = 0;
            mySpriteRenderer.sprite = leverUpSprite;
        }

        // invoke update movement event using current input axis
        updateMovementEvent.Invoke(myCorrespondingPart, inputAxis);
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

    #region Public Methods

    /// <summary>
    /// Adds listener to this object's update movement event
    /// </summary>
    /// <param name="listener"></param>
    public void AddUpdateMovementListener(UnityAction<SubmarineParts, int> listener)
    {
        updateMovementEvent.AddListener(listener);
        Debug.Log(myCorrespondingPart);
    }

    #endregion

}
