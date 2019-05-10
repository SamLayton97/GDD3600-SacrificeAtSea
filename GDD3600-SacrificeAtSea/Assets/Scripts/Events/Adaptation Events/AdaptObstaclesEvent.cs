using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event used to adapt undersea obstacle spawn rate and
/// accurracy according to player performance.
/// </summary>
public class AdaptObstaclesEvent : UnityEvent<float, int>
{
}
