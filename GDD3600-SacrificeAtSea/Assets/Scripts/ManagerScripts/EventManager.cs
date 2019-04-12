﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manager to facilitate invoking and listening for events
/// </summary>
public static class EventManager
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

    #region Updating Submarine Movement Input

    // declare lists holding invokers and listeners of update movement input event
    static List<SubmarineControls> updateMovementInvokers = new List<SubmarineControls>();
    static List<UnityAction<SubmarineParts, int>> updateMovementListeners = new List<UnityAction<SubmarineParts, int>>();

    // Adds given submarine controls as update movement invoker
    public static void AddUpdateMovementInvoker(SubmarineControls invoker)
    {
        // add invoker to list of invokers and add all listeners to invoker
        updateMovementInvokers.Add(invoker);
        foreach (UnityAction<SubmarineParts, int> listener in updateMovementListeners)
        {
            invoker.AddUpdateMovementListener(listener);
        }
    }

    // Adds given method as update movement listener
    public static void AddUpdateMovementListener(UnityAction<SubmarineParts, int> listener)
    {
        // add listener to list and to all invokers
        updateMovementListeners.Add(listener);
        foreach (SubmarineControls invoker in updateMovementInvokers)
        {
            invoker.AddUpdateMovementListener(listener);
        }
    }

    #endregion

}
