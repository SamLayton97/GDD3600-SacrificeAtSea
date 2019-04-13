using System.Collections;
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

    #region Submarine Collision

    // declare lists to hold invokers and listeners to submarine collision event
    static List<SubmarineCollisions> submarineCollisionInvokers = new List<SubmarineCollisions>();
    static List<UnityAction> submarineCollisionListeners = new List<UnityAction>();

    // Adds given submarine collisions script as invoker of event
    public static void AddSubmarineCollisionInvoker(SubmarineCollisions invoker)
    {
        // add invoker to list of invokers and add all listeners to invoker
        submarineCollisionInvokers.Add(invoker);
        foreach (UnityAction listener in submarineCollisionListeners)
        {
            invoker.AddCollisionListener(listener);
        }
    }

    // Adds given method as Submarine Collision listener
    public static void AddSubmarineCollisionListener(UnityAction listener)
    {
        // adds listener to list and to all invokers
        submarineCollisionListeners.Add(listener);
        foreach (SubmarineCollisions invoker in submarineCollisionInvokers)
        {
            invoker.AddCollisionListener(listener);
        }
    }

    #endregion

    #region Update Hull Integrity

    // declare lists holding invokers and listeners to update hull event
    static List<SubmarineHealthManager> updateHullInvokers = new List<SubmarineHealthManager>();
    static List<UnityAction<int>> updateHullListeners = new List<UnityAction<int>>();

    // Adds given health manager as invoker of update hull event
    public static void AddHullIntegrityInvoker(SubmarineHealthManager invoker)
    {
        // add invoker to list of invokers and add all listeners to invoker
        updateHullInvokers.Add(invoker);
        foreach (UnityAction<int> listener in updateHullListeners)
        {
            invoker.AddUpdateHullIntegrityListener(listener);
        }
    }

    // Add given method as listener to update hull event
    public static void AddHullIntegrityListener(UnityAction<int> listener)
    {
        // add listener to list and to all invokers
        updateHullListeners.Add(listener);
        foreach (SubmarineHealthManager invoker in updateHullInvokers)
        {
            invoker.AddUpdateHullIntegrityListener(listener);
        }
    }

    #endregion

    #region Increment Level Progress

    // declare lists to hold invokers and listeners to increment level progress event
    static List<ProgressManager> incrementProgressInvokers = new List<ProgressManager>();
    static List<UnityAction> incrementProgressListeners = new List<UnityAction>();

    // Adds given progress manager as invoker of increment progress event
    public static void AddIncrementProgressInvoker(ProgressManager invoker)
    {
        // adds invoker to list and adds all listeners to this event
        incrementProgressInvokers.Add(invoker);
        foreach (UnityAction listener in incrementProgressListeners)
        {
            invoker.AddIncrementProgressListener(listener);
        }
    }

    // Adds given method as listener to increment progress event
    public static void AddIncrementProgressListener(UnityAction listener)
    {
        // adds listener to list and to all invokers of event
        incrementProgressListeners.Add(listener);
        foreach (ProgressManager invoker in incrementProgressInvokers)
        {
            invoker.AddIncrementProgressListener(listener);
        }
    }

    #endregion

}
