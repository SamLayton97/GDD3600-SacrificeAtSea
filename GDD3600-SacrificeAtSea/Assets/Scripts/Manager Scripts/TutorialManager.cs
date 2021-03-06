﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Facilitates tutorial's sequencing
/// </summary>
public class TutorialManager : MonoBehaviour
{
    #region Fields

    // phase support variables
    TutorialStages currentStage = TutorialStages.InitialSteps;

    // audio feedback support
    [SerializeField] AudioSource volleyDodgedAudioSource;
    [SerializeField] AudioSource endOfTutorialAudioSource;

    // "Return to repair" phase support variables
    [SerializeField] DegradingPart[] degradingNavigationParts;
    [SerializeField] GameObject messageTerminalToRemove;
    [SerializeField] GameObject messageTerminalToSpawn;
    PartsManager myPartsManager;
    float returnToRepairDelay = .5f;
    float returnToRepairCounter = 0;

    // End of tutorial support variables
    [SerializeField] GameObject endOfTutorialPanel;

    // event support
    SpawnMineVolleyEvent spawnMineVolleyEvent;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // get reference to object's parts manager
        myPartsManager = GetComponent<PartsManager>();

        // add self as invoker to spawn mine volley event
        spawnMineVolleyEvent = new SpawnMineVolleyEvent();
        EventManager.AddMineVolleyInvoker(this);

        // add self as listener to respective events
        EventManager.AddTriggerNextStageListener(TriggerNextStage);
        EventManager.AddSubmarineCollisionListener(RetryVolley);
        EventManager.AddDodgedVolleyListener(EndVolley);
    }

    // Update is called once per frame
    void Update()
    {
        // if player has reached "return to repair" phase
        if (currentStage == TutorialStages.RepairAgain)
        {
            // if player has returned to repair both navigation terminals
            if (myPartsManager.GetPartFunctionality(SubmarineParts.ballastTanks)
                && myPartsManager.GetPartFunctionality(SubmarineParts.propellors)
                && returnToRepairCounter >= returnToRepairDelay)
            {
                // transition to end of tutorial stage
                TriggerNextStage(TutorialStages.End);
            }

            // increment return to repair delay counter
            returnToRepairCounter += Time.deltaTime;
        }        
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds given method as listener to spawn mine volley event
    /// </summary>
    /// <param name="listener"></param>
    public void AddSpawnMineVolleyListener(UnityAction<int> listener)
    {
        spawnMineVolleyEvent.AddListener(listener);
    }

    #endregion

    #region Tutorial Stage Transitions

    /// <summary>
    /// Triggers next stage of tutorial
    /// </summary>
    /// <param name="nextStage">stage to enter</param>
    void TriggerNextStage(TutorialStages nextStage)
    {
        // if triggered stage is past current, enter it
        if (nextStage > currentStage)
        {
            // update current stage to one triggered
            currentStage = nextStage;

            // enter triggered stage
            switch (nextStage)
            {
                case TutorialStages.InitialSteps:
                    Debug.Log("ERROR: Uninitialized trigger.");
                    break;
                case TutorialStages.Introduction:
                    Debug.Log("Enter Introduction");
                    break;
                case TutorialStages.FirstRepair:
                    Debug.Log("Enter first repair");
                    break;
                case TutorialStages.RepairRest:
                    Debug.Log("Enter repair rest");
                    break;
                case TutorialStages.FirstMineVolley:
                    Debug.Log("Enter first mine volley");
                    EnterFirstMineVolley();
                    break;
                case TutorialStages.SecondMineVolley:
                    EnterSecondMineVolley();
                    Debug.Log("Enter second mine volley");
                    break;
                case TutorialStages.ThirdMineVolley:
                    EnterThirdMineVolley();
                    Debug.Log("Enter third mine volley");
                    break;
                case TutorialStages.RepairAgain:
                    Debug.Log("Enter repair again");
                    EnterRepairAgain();
                    break;
                case TutorialStages.End:
                    Debug.Log("Enter end");
                    EnterTutorialEnd();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Ends current volley of mines when player succeeds in dodging them,
    /// thus transitioning to the next tutorial stage.
    /// </summary>
    void EndVolley()
    {
        // clear nav panel of remaining tutorial mines
        GameObject[] remainingMines = GameObject.FindGameObjectsWithTag("CavernObstacle");
        for (int i = 0; i < remainingMines.Length; i++)
            Destroy(remainingMines[i]);

        // transition to next tutorial stage
        TriggerNextStage((TutorialStages)Mathf.Min((int)currentStage + 1, (int)TutorialStages.RepairAgain));

        // play volley dodged sound effect
        volleyDodgedAudioSource.Play();
    }

    /// <summary>
    /// Restarts current volley of mines when player collidies with
    /// a mine (thus failing to dodge the volley).
    /// </summary>
    void RetryVolley()
    {
        // clear nav panel of remaining tutorial mines
        GameObject[] remainingMines = GameObject.FindGameObjectsWithTag("CavernObstacle");
        for (int i = 0; i < remainingMines.Length; i++)
            Destroy(remainingMines[i]);

        // retry volley corresponding to current tutorial stage
        switch (currentStage)
        {
            case TutorialStages.FirstMineVolley:
                EnterFirstMineVolley();
                break;
            case TutorialStages.SecondMineVolley:
                EnterSecondMineVolley();
                break;
            case TutorialStages.ThirdMineVolley:
                EnterThirdMineVolley();
                break;
            default:
                Debug.Log("Error: Attempting to retry a volley from a non-volley tutorial stage.");
                break;
        }
    }

    /// <summary>
    /// Enters first mine volley stage, telling nav panel to spawn single mine.
    /// </summary>
    void EnterFirstMineVolley()
    {
        spawnMineVolleyEvent.Invoke(1);
    }

    /// <summary>
    /// Enters second mine volley stage, telling nav panel to spawn mines from 2 directions.
    /// </summary>
    void EnterSecondMineVolley()
    {
        spawnMineVolleyEvent.Invoke(2);
    }

    /// <summary>
    /// Enters third mine volley stage, telling nav panel to spawn mines from 2 unexpected directions.
    /// </summary>
    void EnterThirdMineVolley()
    {
        spawnMineVolleyEvent.Invoke(3);
    }

    /// <summary>
    /// Enters repair again stage, causing submarine parts to malfunction and need repair.
    /// </summary>
    void EnterRepairAgain()
    {
        // for each of the degrading parts, set their current health to 0
        for (int i = 0; i < degradingNavigationParts.Length; i++)
            degradingNavigationParts[i].CurrentHealth = 0;

        // remove and replace submarine controls message terminal
        Vector3 messageTerminalSpawnPosition = messageTerminalToRemove.transform.position;
        Destroy(messageTerminalToRemove);
        Instantiate(messageTerminalToSpawn, messageTerminalSpawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Enters tutorial's final stage, spawning a menu that transitions 
    /// player to main gameplay scenes
    /// </summary>
    void EnterTutorialEnd()
    {
        // create instance of end of tutorial panel
        Instantiate(endOfTutorialPanel);

        // play positive feedback sound
        endOfTutorialAudioSource.Play();
    }

    #endregion

}
