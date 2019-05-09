using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event triggered when GM runs intermediary evaluation, scaling spawn
/// rate of obstacles on the navigation panel.
/// </summary>
public class ScaleObstacleRateEvent : UnityEvent<float>
{
}
