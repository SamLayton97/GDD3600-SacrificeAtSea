using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event called by sequencers to trigger next stage
/// of tutorial.
/// </summary>
public class TriggerNextStageEvent : UnityEvent<TutorialStages>
{
}
