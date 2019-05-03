using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls setting of text on a in-game message panel
/// </summary>
public class InGameMessage : MonoBehaviour
{
    // text setting support
    [SerializeField] Text messageText;

    // tutorial-specific messages
    [SerializeField] string introductionMessage;
    [SerializeField] string firstRepairMessage;
    [SerializeField] string repairRestMessage;
    [SerializeField] string submarineControlsMessage;
    [SerializeField] string returnToRepairMessage;
    [SerializeField] string defaultMessage;

    /// <summary>
    /// Write-access property to set string
    /// displayed by this message box.
    /// </summary>
    public string MessageBoxText
    {
        set
        {
            messageText.text = value;
        }
    }

    /// <summary>
    /// Sets displayed message according to given stage of tutorial
    /// </summary>
    /// <param name="tutorialStage">stage of tutorial determining
    /// what message to display</param>
    public void SetTutorialMessage(TutorialStages tutorialStage)
    {
        // display message appropriate to stage of tutorial
        switch (tutorialStage)
        {
            case TutorialStages.Introduction:
                messageText.text = introductionMessage;
                break;
            case TutorialStages.FirstRepair:
                messageText.text = firstRepairMessage;
                break;
            case TutorialStages.RepairRest:
                messageText.text = repairRestMessage;
                break;
            case TutorialStages.FirstMineVolley:
                messageText.text = submarineControlsMessage;
                break;
            case TutorialStages.RepairAgain:
                messageText.text = returnToRepairMessage;
                break;
            default:
                messageText.text = defaultMessage;
                break;
        }

    }
}
