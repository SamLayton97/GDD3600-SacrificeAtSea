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

    #region Swapping Camera's Target

    // declare lists holding invokers and listeners of event
    static List<CameraTargetSwapper> cameraSwapInvokers = new List<CameraTargetSwapper>();
    static List<UnityAction<Transform>> cameraSwapListeners = new List<UnityAction<Transform>>();

    /// <summary>
    /// Adds given target-swapper as invoker of this event
    /// </summary>
    /// <param name="invoker">a camera target swapper</param>
    public static void AddCameraSwapInvoker(CameraTargetSwapper invoker)
    {
        // add invoker to list of invokers and add all listeners to invoker
        cameraSwapInvokers.Add(invoker);
        foreach (UnityAction<Transform> listener in cameraSwapListeners)
        {
            invoker.AddSwapCameraTargetListener(listener);
        }
    }

    /// <summary>
    /// Adds given method as listener for swap camera event
    /// </summary>
    /// <param name="listener"></param>
    public static void AddCameraSwapListener(UnityAction<Transform> listener)
    {
        // add listener to list and to all invokers
        cameraSwapListeners.Add(listener);
        foreach (CameraTargetSwapper invoker in cameraSwapInvokers)
        {
            invoker.AddSwapCameraTargetListener(listener);
        }
    }

    #endregion
}
