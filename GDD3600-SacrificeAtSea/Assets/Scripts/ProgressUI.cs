using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Script used to update user-interface tracking submarine's
/// health and progress towards goal
/// </summary>
public class ProgressUI : MonoBehaviour
{
    // hull integrity support
    [SerializeField] Text hullIntegrityText;
    const string HullIntegrityTextPrefix = "HULL: ";
    int hullIntegrity = 100;

    // level progress support
    [SerializeField] Text goalProgressText;
    const string GoalProgressTextPrefix = "PROGRESS: ";
    int goalProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        // initialize UI text
        hullIntegrityText.text = HullIntegrityTextPrefix + hullIntegrity;
        goalProgressText.text = GoalProgressTextPrefix + goalProgress;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
