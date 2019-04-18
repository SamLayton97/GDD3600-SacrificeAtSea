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
    Vector2 forceVector = new Vector2();
    Rigidbody2D rigidbody2D;

    // force scaling variables
    [SerializeField] Vector2 forceScale;

    // "sway" variables
    Vector2 swayForceVector = new Vector2();
    [SerializeField] PartsManager partsManager;
    [SerializeField] Vector2 swayForceScale;
    [SerializeField] float maxSwayRotation = 5f;
    
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
        // calculate force vector to move submarine towards
        forceVector = inputVector;
        forceVector.Scale(forceScale);

        // if submarine's stabilizer has malfunctioned, calculate sway
        if (!partsManager.GetPartFunctionality(SubmarineParts.stabalizer))
        {
            // calculate current direction of sub's velocity
            float currRotation = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x);

            // calculate "sway" velocity with high probablitiy of being similar in direction to current
            float newRotation = currRotation + (Random.Range(-maxSwayRotation, maxSwayRotation) * Mathf.Deg2Rad);
            swayForceVector.x = Mathf.Cos(newRotation);
            swayForceVector.y = Mathf.Sin(newRotation);
            swayForceVector.Normalize();
            swayForceVector.Scale(swayForceScale);
        }
        // otherwise (stabilizer is working)
        else
            // set sway force to zero vector
            swayForceVector = Vector2.zero;
        
    }

    /// <summary>
    /// Called fixed number of times per second. Used for applying
    /// forces to submarine navigation icon.
    /// </summary>
    void FixedUpdate()
    {
        // add force in direction of input vector
        rigidbody2D.AddForce(forceVector);

        // apply "sway" force
        rigidbody2D.AddForce(swayForceVector);
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
