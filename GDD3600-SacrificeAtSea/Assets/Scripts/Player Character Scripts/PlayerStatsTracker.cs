using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks data specific to the player-character
/// </summary>
public class PlayerStatsTracker : MonoBehaviour
{
    // idle time support variables
    float idleTime = 0f;
    Rigidbody2D myRigidbody2D;

    /// <summary>
    /// Read-access property returning ratio of time player
    /// was motionless over total time during level.
    /// </summary>
    public float IdleTimeRatio
    {
        get { return idleTime / Time.timeSinceLevelLoad; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // retrieve reference to rigidbody component
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player is motionless, increment idle time
        if (myRigidbody2D.velocity.x == 0 && myRigidbody2D.velocity.y == 0)
            idleTime += Time.deltaTime;
    }
}
