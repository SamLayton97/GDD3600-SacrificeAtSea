using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Facilitates tutorial's sequencing
/// </summary>
public class TutorialManager : MonoBehaviour
{
    // phase support variables
    TutorialStages currentStage = TutorialStages.InitialSteps;

    // event support
    SpawnMineVolleyEvent spawnMineVolleyEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker to spawn mine volley event
        spawnMineVolleyEvent = new SpawnMineVolleyEvent();
        EventManager.AddMineVolleyInvoker(this);

        // add self as listener to trigger next stage event
        EventManager.AddTriggerNextStageListener(TriggerNextStage);
    }

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
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Adds given method as listener to spawn mine volley event
    /// </summary>
    /// <param name="listener"></param>
    public void AddSpawnMineVolleyListener(UnityAction<int> listener)
    {
        spawnMineVolleyEvent.AddListener(listener);
    }

    #region Tutorial Stage Transitions

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
    /// Enters third mine volley stage, telling nav panel to spawn mines from 3 directions.
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

    }

    #endregion

}
