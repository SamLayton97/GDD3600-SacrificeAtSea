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
