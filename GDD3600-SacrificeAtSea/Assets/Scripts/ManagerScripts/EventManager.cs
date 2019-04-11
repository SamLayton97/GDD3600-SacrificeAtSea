using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manager to facilitate invoking and listening for events
/// </summary>
public class EventManager
{
    #region Updating Part Functionality

    // declare lists holding invokers and listeners of event
    static List<DegradingPart> updateFunctionalityInvokers = new List<DegradingPart>();
    static List<UnityAction<SubmarineParts, bool>> updateFunctionalityListeners =
            new List<UnityAction<SubmarineParts, bool>>();

    /// <summary>
    /// Adds given degrading part as invoker of this event
    /// </summary>
    /// <param name="invoker">a degrading submarine part</param>
    public static void AddUpdateFunctionalityInvoker(DegradingPart invoker)
    {
        // add invoker to list of invokers and add all listeners to invoker
        updateFunctionalityInvokers.Add(invoker);
        foreach (UnityAction<SubmarineParts, bool> listener in updateFunctionalityListeners)
        {
            invoker.AddUpdateFunctionalityListener(listener);
        }
    }

    /// <summary>
    /// Adds given method as an update functionality listener
    /// </summary>
    /// <param name="listener"></param>
    public static void AddUpdateFunctionalityListeners(UnityAction<SubmarineParts, bool> listener)
    {
        // add listener to list and to all invokers
        updateFunctionalityListeners.Add(listener);
        foreach (DegradingPart invoker in updateFunctionalityInvokers)
        {
            invoker.AddUpdateFunctionalityListener(listener);
        }
    }

    #endregion
}
