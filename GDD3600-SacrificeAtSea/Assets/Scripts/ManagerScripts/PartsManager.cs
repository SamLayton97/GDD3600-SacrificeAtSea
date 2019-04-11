using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script controlling which submarine parts are currently enabled or disabled
/// </summary>
public class PartsManager : MonoBehaviour
{
    // private variables
    Dictionary<SubmarineParts, bool> partFunctionalities = new Dictionary<SubmarineParts, bool>();

    /// <summary>
    /// Called just before Start()
    /// </summary>
    private void Awake()
    {
        // load part functionality dictionary with part names and flags determining whether they work
        for (int i = 0; i < 4; i++)
        {
            partFunctionalities.Add((SubmarineParts)i, true);
        }

        // add self as listener for update functionality event
        EventManager.AddUpdateFunctionalityListeners(UpdatePartFunctionality);
    }

    /// <summary>
    /// Gets whether a submarine part is currently functioning
    /// </summary>
    /// <param name="part">name of part</param>
    /// <returns>bool for whether it is functioning</returns>
    public bool GetPartFunctionality(SubmarineParts partName)
    {
        // return functionality of corresponding part
        return partFunctionalities[partName];
    }

    /// <summary>
    /// Sets whether a submarine part is currently functioning
    /// </summary>
    /// <param name="partName">name of part to update functionality of</param>
    /// <param name="isFunctioning">whether part now functions</param>
    void UpdatePartFunctionality(SubmarineParts partName, bool isFunctioning)
    {
        // update part's functionality flag
        partFunctionalities[partName] = isFunctioning;

        // DEBUGGING: log name and functionality of updated submarine part
        Debug.Log(partName + " " + isFunctioning);
    }
}
