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
    Vector2 inputVector = new Vector2();
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        // retrieve rigidbody component from game object
        rigidbody2D = GetComponent<Rigidbody2D>();

        // add self as listener to Update Movement Event
        EventManager.AddUpdateMovementListener(UpdateForceVector);
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUGGING: log input vector
        //Debug.Log(inputVector);
    }

    /// <summary>
    /// Called fixed number of times per second. Used for applying
    /// forces to submarine navigation icon.
    /// </summary>
    void FixedUpdate()
    {
        // add force in direction of input vector
        rigidbody2D.AddForce(inputVector);
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
            inputVector.x = inputAxis;
        else if (moveType == SubmarineParts.ballastTanks)
            inputVector.y = inputAxis;
    }
}
