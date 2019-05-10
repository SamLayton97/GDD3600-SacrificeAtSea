using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages modification of various difficulty variables
/// to adapt to player's performance.
/// </summary>
public class AdaptationManager : MonoBehaviour
{
    // performance data collection support variables
    ProgressManager myProgressManager;
    SubmarineHealthManager mySubmarineHealthManager;
    TreasureCollectionManager myTreasureCollectionManager;
    [SerializeField] PlayerStatsTracker playerStatsTracker;

    // adaptation support variables
    [SerializeField] UnderseaObjectSpawner underseaObjectSpawner;
    [SerializeField] SubmarineMovement submarineMovement;

    // event support
    AdaptObstaclesEvent adaptObstaclesEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of adapt obstacles event
        adaptObstaclesEvent = new AdaptObstaclesEvent();
        EventManager.AddAdaptObstaclesInvoker(this);
        
        // retrieve references to data-collection components
        myProgressManager = GetComponent<ProgressManager>();
        mySubmarineHealthManager = GetComponent<SubmarineHealthManager>();
        myTreasureCollectionManager = GetComponent<TreasureCollectionManager>();

        // read performance data and change in-game variables accordingly
        ReadAndAdapt();
    }

    /// <summary>
    /// Reads performance data from previous scene
    /// and adapts accordingly.
    /// </summary>
    void ReadAndAdapt()
    {
        // if player performance is initialized, adapt various variables to player performance
        if (InterSceneInformationHandler.Instance.DataIsInitialized)
        {

        }
    }

    /// <summary>
    /// Saves player performance-tracking data from
    /// current level.
    /// </summary>
    public void SavePerformanceData()
    {
        // set player-performance data for next scene
        InterSceneInformationHandler.Instance.IdleTimeRatio = playerStatsTracker.IdleTimeRatio;
        InterSceneInformationHandler.Instance.AdaptabilityRating = myProgressManager.AverageAdaptability;
        InterSceneInformationHandler.Instance.HighestPartHealth = myProgressManager.HighestFunctioningPart;
        InterSceneInformationHandler.Instance.FinalDamageTaken = mySubmarineHealthManager.DamageTaken;
        InterSceneInformationHandler.Instance.OpportunitiesForTreasure = myProgressManager.NumberOfMidLevelEvals;
        InterSceneInformationHandler.Instance.TreasureCollected = myTreasureCollectionManager.TreasureCollected;
    }

    /// <summary>
    /// Adds given listener to this object's adapt obstacles event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddAdaptObstaclesListener(UnityAction<float, int> newListener)
    {
        adaptObstaclesEvent.AddListener(newListener);
    }
}
