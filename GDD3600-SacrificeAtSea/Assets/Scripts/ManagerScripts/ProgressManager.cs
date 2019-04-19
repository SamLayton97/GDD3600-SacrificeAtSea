﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Script managing player's progress towards level's end
/// </summary>
public class ProgressManager : MonoBehaviour
{
    #region Fields

    // level time fields
    [SerializeField] float levelLength = 60;    // time it takes to complete level in seconds

    // progress incrementation support variables
    int currentProgressPercent = 0;
    float timeToIncrementPercentage;
    float percentIncrementCounter = 0;

    // intermediary evaluation support
    [SerializeField] int numberOfEvaluationsPerLevel = 3;
    bool isUnscathed = true;
    int[] evaluationPoints;

    // event support
    IncrementProgressEvent incrementProgressEvent;
    SpawnTreasureEvent spawnTreasureEvent;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // calculate time to between progress percent increments
        timeToIncrementPercentage = levelLength / 100;

        // adds self as invoker of respective events
        incrementProgressEvent = new IncrementProgressEvent();
        EventManager.AddIncrementProgressInvoker(this);
        spawnTreasureEvent = new SpawnTreasureEvent();
        EventManager.AddSpawnTreasureInvoker(this);

        // adds self as listener to respective event(s)
        EventManager.AddSubmarineCollisionListener(HandleSubmarineCollision);

        // find evaluation points in level
        evaluationPoints = FindEvaluationPoints(numberOfEvaluationsPerLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // if counter exceeds time to increment percentage
        if (percentIncrementCounter >= timeToIncrementPercentage)
        {
            // reset counter
            percentIncrementCounter = 0;

            // increment progress percentage (locked to 100%)
            currentProgressPercent = Mathf.Min(100, currentProgressPercent + 1);
            incrementProgressEvent.Invoke();

            // if current progress corresponds to one of intermediary evaluation points
            if (Array.Exists(evaluationPoints, element => element == currentProgressPercent))
            {
                // run intermediary evaluation
                RunIntermediaryEvaluation();
            }

            // if player reaches 100% level progress, they win!
            if (currentProgressPercent >= 100)
            {
                // go to level complete scene
                SceneManager.LoadScene("LevelCompleteScene");
            }
        }

        // increment counter
        percentIncrementCounter += Time.deltaTime;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Finds progress percentages to run intermediary evaluation
    /// </summary>
    /// <param name="evalCount">number of intermediary evaluations to run</param>
    /// <returns>array of percentages corresponding to points in level
    /// to run intermediary evaluation</returns>
    int[] FindEvaluationPoints(int evalCount)
    {
        // create empty list to store eval points
        int[] evalPoints = new int[evalCount];

        // calculate spacing between evaluation points
        int evalSpacing = 100 / (evalCount + 1);

        // for as many eval points as there ought to be in level
        for (int i = 0; i < evalCount; i++)
        {
            // add evaluation point spaced evenly through level
            evalPoints[i] = evalSpacing * (i + 1);
        }

        // return array of eval points
        return evalPoints;
    }

    /// <summary>
    /// Runs intermediary assesment of player's performance, spawning rewards
    /// and adjusting in-game variables as appropriate.
    /// </summary>
    void RunIntermediaryEvaluation()
    {
        // if player made it through portion of level unscathed
        if (isUnscathed)
        {
            // invoke "Spawn Treasure" event
            spawnTreasureEvent.Invoke();
        }

        // reset performance tracking variables
        isUnscathed = true;
    }

    /// <summary>
    /// Listens for submarine collision event, setting unscathed flag to
    /// false if triggered.
    /// </summary>
    void HandleSubmarineCollision()
    {
        // set 'unscathed' flag to false for this section of level
        isUnscathed = false;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds given listener to object's increment progress event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddIncrementProgressListener(UnityAction newListener)
    {
        incrementProgressEvent.AddListener(newListener);
    }

    /// <summary>
    /// Adds given listener to object's spawn treasure event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddSpawnTreasureListener(UnityAction newListener)
    {
        spawnTreasureEvent.AddListener(newListener);
    }

    #endregion
}
