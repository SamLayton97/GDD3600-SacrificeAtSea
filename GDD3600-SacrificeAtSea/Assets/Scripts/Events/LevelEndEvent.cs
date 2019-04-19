using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event triggered when player has reached one of level's end conditions.
/// </summary>
public class LevelEndEvent : UnityEvent<int, int, int, float>
{
}
