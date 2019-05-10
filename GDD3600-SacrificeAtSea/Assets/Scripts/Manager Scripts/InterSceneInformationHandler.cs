using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton storing player-performance information across scenes.
/// </summary>
public class InterSceneInformationHandler : MonoBehaviour
{
    // define singleton support variables
    public static InterSceneInformationHandler Instance;

    // define player-performance data variables
    public float IdleTimeRatio = -1f;
    public float AdaptabilityRating = -1f;
    public float HighestPartHealth = -1f;
    public int FinalDamageTaken = -1;
    public int OpportunitiesForTreasure = -1;
    public int TreasureCollected = -1;

    /// <summary>
    /// Read-access property returning whether data has been set
    /// to anything outside of safe defaults.
    /// </summary>
    public bool DataIsInitialized
    {
        get
        {
            // if any variable is set to default, data isn't initialized
            if (IdleTimeRatio == -1 || AdaptabilityRating == -1 || HighestPartHealth == -1
                || FinalDamageTaken == -1 || OpportunitiesForTreasure == -1 || TreasureCollected == -1)
                return false;
            // otherwise, data is initialized
            else
                return true;
        }
    }

    // Initializes information handler singleton
    void Awake()
    {
        // if singleton instance has been initialized yet
        if (Instance == null)
        {
            // set this instance of info handler
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        // otherwise, destroy new copy of singleton
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
}
