﻿using System.Collections;
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
    int startingHullIntegrity = 100;

    // level progress support
    [SerializeField] Text goalProgressText;
    const string GoalProgressTextPrefix = "PROGRESS: ";
    int currGoalProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        // initialize UI text
        hullIntegrityText.text = HullIntegrityTextPrefix + startingHullIntegrity;
        goalProgressText.text = GoalProgressTextPrefix + currGoalProgress;

        // add self as listener to respective events
        EventManager.AddHullIntegrityListener(UpdateHullIntegrity);
        EventManager.AddIncrementProgressListener(UpdateLevelProgress);
    }

    // Updates UI components related to hull integrity
    void UpdateHullIntegrity(int newIntegrity)
    {
        // update hull integrity text
        hullIntegrityText.text = HullIntegrityTextPrefix + newIntegrity;
    }

    // Updates UI components related to level progress
    void UpdateLevelProgress()
    {
        // update level progress text
        currGoalProgress++;
        goalProgressText.text = GoalProgressTextPrefix + Mathf.Min(100, currGoalProgress);
    }
}