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

    /// <summary>
    /// Read-access property returning current stage of tutorial
    /// </summary>
    TutorialStages CurrentStage
    {
        get { return currentStage; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
