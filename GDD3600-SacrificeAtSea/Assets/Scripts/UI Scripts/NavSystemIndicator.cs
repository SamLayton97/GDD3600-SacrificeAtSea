using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Environmental sign that indicates whether its corresponding 
/// nav capabilities are working.
/// </summary>
public class NavSystemIndicator : MonoBehaviour
{
    // functionality support
    [SerializeField] SubmarineParts myCorrespondingPart = SubmarineParts.ballastTanks;

    // sprite swapping support
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite enabledSprite;
    [SerializeField] Sprite disabledSprite;

    // Start is called before the first frame update
    void Start()
    {
        // retrieve sprite renderer component from game object
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // add self as listener to Update Part Functionality event
        EventManager.AddUpdateFunctionalityListeners(UpdateFunctionality);
    }

    // Updates sprite to reflect whether its corresponding part is enabled
    void UpdateFunctionality(SubmarineParts updatedPart, bool isEnabled)
    {
        // if updated part matches part indicator represents
        if (updatedPart == myCorrespondingPart)
        {
            // set indicator sprite to match updated functionality
            mySpriteRenderer.sprite = enabledSprite;
            if (!isEnabled)
                mySpriteRenderer.sprite = disabledSprite;
        }
    }
}
