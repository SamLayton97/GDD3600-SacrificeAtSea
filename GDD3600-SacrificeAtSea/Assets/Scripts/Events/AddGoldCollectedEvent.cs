using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event triggered when UI must update to reflect new
/// collected gold amount.
/// </summary>
public class AddGoldCollectedEvent : UnityEvent<int>
{
}
