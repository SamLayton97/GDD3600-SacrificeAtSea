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
    public float IdleTimeRatio = 0f;
    public float AdaptabilityRating = 0f;
    public float HighestPartHealth = 0f;
    public int FinalDamageTaken = 0;
    public int OpportunitiesForTreasure = 0;
    public int TreasureCollected = 0;

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
