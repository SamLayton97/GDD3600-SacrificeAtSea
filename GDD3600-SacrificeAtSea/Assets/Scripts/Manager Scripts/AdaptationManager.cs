using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
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

    }

    /// <summary>
    /// Saves player performance-tracking data from
    /// current level.
    /// </summary>
    void SavePerformanceData()
    {
        // set player-performance data for next scene
        InterSceneInformationHandler.Instance.IdleTimeRatio = playerStatsTracker.IdleTimeRatio;
        InterSceneInformationHandler.Instance.FinalDamageTaken = mySubmarineHealthManager.DamageTaken;
        InterSceneInformationHandler.Instance.OpportunitiesForTreasure = myProgressManager.NumberOfEvalsPerLevel;
        InterSceneInformationHandler.Instance.TreasureCollected = myTreasureCollectionManager.TreasureCollected;
    }
}
