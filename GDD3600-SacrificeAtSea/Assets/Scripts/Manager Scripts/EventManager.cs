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

    #region Update Sonar Visibility

    // declare lists to hold invokers and listener to update visibility event
    static List<SonarVisibilityController> updateVisibilityInvokers = new List<SonarVisibilityController>();
    static List<UnityAction<float>> updateVisibilityListeners = new List<UnityAction<float>>();

    // Adds given visibility controller as invoker of update visibility event
    public static void AddUpdateVisibilityInvokers(SonarVisibilityController invoker)
    {
        // adds invoker to list and adds all listeners to this event
        updateVisibilityInvokers.Add(invoker);
        foreach (UnityAction<float> listener in updateVisibilityListeners)
        {
            invoker.AddUpdateVisibilityListener(listener);
        }
    }

    // Adds given method as listener to update visibility event
    public static void AddUpdateVisibilityListener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        updateVisibilityListeners.Add(listener);
        foreach (SonarVisibilityController invoker in updateVisibilityInvokers)
        {
            invoker.AddUpdateVisibilityListener(listener);
        }
    }

    #endregion

    #region Spawn Treasure

    // declare lists to hold invokers and listeners to spawn treasure event
    static List<ProgressManager> spawnTreasureInvokers = new List<ProgressManager>();
    static List<UnityAction> spawnTreasureListeners = new List<UnityAction>();

    // Adds given progress manager as invoker of spawn treasure event
    public static void AddSpawnTreasureInvoker(ProgressManager invoker)
    {
        // adds invoker to list and adds all listeners to this event
        spawnTreasureInvokers.Add(invoker);
        foreach (UnityAction listener in spawnTreasureListeners)
        {
            invoker.AddSpawnTreasureListener(listener);
        }
    }

    // Adds given method as listner to spawn treasure event
    public static void AddSpawnTreasureListener(UnityAction listener)
    {
        // adds listener to list and to all invokers of event
        spawnTreasureListeners.Add(listener);
        foreach (ProgressManager invoker in spawnTreasureInvokers)
        {
            invoker.AddSpawnTreasureListener(listener);
        }
    }

    #endregion

    #region Collect Treasure

    // declare lists to hold invokers and listeners to collect treasure event
    static List<SubmarineCollisions> collectTreasureInvokers = new List<SubmarineCollisions>();
    static List<UnityAction> collectTreasureListeners = new List<UnityAction>();

    // Adds given submarine collisions script as invoker of collect treasure event
    public static void AddCollectTreasureInvoker(SubmarineCollisions invoker)
    {
        // adds invoker to list and adds all listeners to this event
        collectTreasureInvokers.Add(invoker);
        foreach (UnityAction listener in collectTreasureListeners)
        {
            invoker.AddCollectTreasureListener(listener);
        }
    }

    // Adds given method as listener to collect treasure event
    public static void AddCollectTreasureListener(UnityAction listener)
    {
        // adds listener to list and to all invokers of event
        collectTreasureListeners.Add(listener);
        foreach (SubmarineCollisions invoker in collectTreasureInvokers)
        {
            invoker.AddCollectTreasureListener(listener);
        }
    }

    #endregion

    #region Add Gold Collected

    // declare lists to hold invokers and listeners to add gold event
    static List<TreasureCollectionManager> addGoldInvokers = new List<TreasureCollectionManager>();
    static List<UnityAction<int>> addGoldListeners = new List<UnityAction<int>>();

    // Adds given treasure collection manager as invoker of add gold event
    public static void AddGoldInvoker(TreasureCollectionManager invoker)
    {
        // adds invoker to list and adds all listeners to this event
        addGoldInvokers.Add(invoker);
        foreach (UnityAction<int> listener in addGoldListeners)
        {
            invoker.AddGoldCollectedListener(listener);
        }
    }

    // Adds given method as listener to add gold event
    public static void AddGoldListener(UnityAction<int> listener)
    {
        // adds listener to list and to all invokers of event
        addGoldListeners.Add(listener);
        foreach (TreasureCollectionManager invoker in addGoldInvokers)
        {
            invoker.AddGoldCollectedListener(listener);
        }
    }

    #endregion

    #region Scale Obstacle Spawn Rate

    // declare lists to hold invokers and listeners to scale obstacle rate event
    static List<ProgressManager> scaleObstacleRateInvokers = new List<ProgressManager>();
    static List<UnityAction<float>> scaleObstacleRateListeners = new List<UnityAction<float>>();

    // Adds given progress manager as invoker of scale obstacle rate event
    public static void AddScaleObstacleRateInvoker(ProgressManager invoker)
    {
        // adds invoker to list and adds all listeners to this event
        scaleObstacleRateInvokers.Add(invoker);
        foreach (UnityAction<float> listener in scaleObstacleRateListeners)
        {
            invoker.AddScaleObstaclesRateListener(listener);
        }
    }

    // Adds given method as listener to scale obstacle rate event
    public static void AddScaleObstacleRateListener(UnityAction<float> listener)
    {
        // adds listener to list and to all invokers of event
        scaleObstacleRateListeners.Add(listener);
        foreach (ProgressManager invoker in scaleObstacleRateInvokers)
        {
            invoker.AddScaleObstaclesRateListener(listener);
        }
    }

    #endregion

    #region Trigger Next Tutorial Stage

    // declare lists to hold invokers and listeners to trigger next stage event
    static List<MessageTerminal> triggerNextStageInvokers = new List<MessageTerminal>();
    static List<UnityAction<TutorialStages>> triggerNextStageListeners = new List<UnityAction<TutorialStages>>();

    // Adds given message terminal as invoker of trigger next stage event
    public static void AddTriggerNextStageInvoker(MessageTerminal invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        triggerNextStageInvokers.Add(invoker);
        foreach (UnityAction<TutorialStages> listener in triggerNextStageListeners)
        {
            invoker.AddTriggerNextStageListener(listener);
        }
    }

    // Adds given method as listener to trigger next stage event
    public static void AddTriggerNextStageListener(UnityAction<TutorialStages> listener)
    {
        // adds listener to list and to all invokers of event
        triggerNextStageListeners.Add(listener);
        foreach (MessageTerminal invoker in triggerNextStageInvokers)
        {
            invoker.AddTriggerNextStageListener(listener);
        }
    }

    #endregion

    #region Spawn Tutorial Mine Volley

    // declare lists to hold invokers and listeners to spawn mine volley event
    static List<TutorialManager> mineVolleyInvokers = new List<TutorialManager>();
    static List<UnityAction<int>> mineVolleyListeners = new List<UnityAction<int>>();

    // Adds given tutorial manager as invoker of spawn mine volley event
    public static void AddMineVolleyInvoker(TutorialManager invoker)
    {
        // adds invoker to list and adds all listeners to this invoker
        mineVolleyInvokers.Add(invoker);
        foreach (UnityAction<int> listener in mineVolleyListeners)
        {
            invoker.AddSpawnMineVolleyListener(listener);
        }
    }

    // Adds given method as listener to spawn mine volley event
    public static void AddMineVolleyListener(UnityAction<int> listener)
    {
        // adds listener to list and to all invokers of event
        mineVolleyListeners.Add(listener);
        foreach (TutorialManager invoker in mineVolleyInvokers)
        {
            invoker.AddSpawnMineVolleyListener(listener);
        }
    }

    #endregion
}
