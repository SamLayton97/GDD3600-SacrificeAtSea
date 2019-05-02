using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A submarine part that player can repair to progress
/// through tutorial.
/// </summary>
public class TutorialRepairTerminal : MonoBehaviour
{
    // repair support variables
    SubmarineParts myPart;

    // tutorial progression support variables
    [SerializeField] GameObject doorToOpen;

    // Start is called before the first frame update
    void Start()
    {
        // initialize submarine part variable to match that of terminal
        myPart = GetComponent<DegradingPart>().MyPart;

        // add self as listener to the "Update Functionality" event
        EventManager.AddUpdateFunctionalityListeners(Activate);
    }

    /// <summary>
    /// Opens door paired with this tutorial repair terminal
    /// </summary>
    /// <param name="updatedPart">part updated by player</param>
    /// <param name="isFunctioning">whether part now functions</param>
    void Activate(SubmarineParts updatedPart, bool isFunctioning)
    {
        // if user fixed part that corresponds to this terminal and terminal has door to open
        if (updatedPart == myPart && isFunctioning && doorToOpen != null)
        {
            // remove/open door blocking player's progress
            Destroy(doorToOpen);
        }
    }
}
