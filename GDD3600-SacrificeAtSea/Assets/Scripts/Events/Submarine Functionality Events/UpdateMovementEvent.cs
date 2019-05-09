using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event to update force-based movement of submarine icon
/// </summary>
public class UpdateMovementEvent : UnityEvent<SubmarineParts, int>
{
}
