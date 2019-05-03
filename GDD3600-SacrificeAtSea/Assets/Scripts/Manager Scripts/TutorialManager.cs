using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Facilitates tutorial's sequencing
/// </summary>
public class TutorialManager : MonoBehaviour
{
    // phase support variables
    TutorialStages currentStage = TutorialStages.InitialSteps;

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to trigger next stage event
        EventManager.AddTriggerNextStageListener(TriggerNextStage);
    }

    // Update is called once per frame
    void Update()
    {
        
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

            // TODO: enter triggered stage
            switch (nextStage)
            {
                case TutorialStages.InitialSteps:
                    Debug.Log("ERROR: Uninitialized trigger.");
                    break;
                case TutorialStages.Introduction:
                    break;
                case TutorialStages.FirstRepair:
                    break;
                case TutorialStages.RepairRest:
                    break;
                case TutorialStages.FirstMineVolley:
                    break;
                case TutorialStages.SecondMineVolley:
                    break;
                case TutorialStages.ThirdMineVolley:
                    break;
                case TutorialStages.RepairAgain:
                    break;
                default:
                    break;
            }
        }
    }
}
