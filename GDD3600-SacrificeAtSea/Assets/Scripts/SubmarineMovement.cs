using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script to control movement of submarine on navigation panel
/// </summary>
public class SubmarineMovement : MonoBehaviour
{
    // movement variables
    Vector2 movementForce = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to Update Movement Event
        EventManager.AddUpdateMovementListener(UpdateForceVector);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movementForce);
    }

    /// <summary>
    /// Updates force to push submarine in
    /// </summary>
    /// <param name="moveType">Part determining axis to move submarine on</param>
    /// <param name="inputAxis">direction to move submarine along given axis</param>
    void UpdateForceVector(SubmarineParts moveType, int inputAxis)
    {
        // updates x or y force component depending on received movement type
        if (moveType == SubmarineParts.propellors)
            movementForce.x = inputAxis;
        else if (moveType == SubmarineParts.ballastTanks)
            movementForce.y = inputAxis;
    }
}
