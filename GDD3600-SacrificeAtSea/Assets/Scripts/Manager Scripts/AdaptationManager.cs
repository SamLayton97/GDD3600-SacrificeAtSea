using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages modification of various difficulty variables
/// to adapt to player's performance.
/// </summary>
public class AdaptationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // read performance data and change in-game variables accordingly
        ReadAndAdapt();
    }

    /// <summary>
    /// Reads performance data from previous scene
    /// and adapts accordingly.
    /// </summary>
    void ReadAndAdapt()
    {
        Debug.Log("READ AND ADAPT");
    }

    /// <summary>
    /// Saves player performance-tracking data from
    /// current level.
    /// </summary>
    void SavePerformanceData()
    {
        Debug.Log("SAVE PERFORMANCE DATA");
    }
}
