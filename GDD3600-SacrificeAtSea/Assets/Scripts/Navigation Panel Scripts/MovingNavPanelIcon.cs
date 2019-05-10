using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script controlling movement of obstacles and treasure on
/// the navigation panel.
/// </summary>
public class MovingNavPanelIcon : MonoBehaviour
{
    // force support
    Rigidbody2D myRigidbody2D;
    [SerializeField] float impulseForceScale = 1f;
    [SerializeField] float forceAngleArc = 35f;
    Transform target;

    /// <summary>
    /// Property with write access containing target's transform info.
    /// </summary>
    public Transform Target
    {
        set { target = value; }
    }

    /// <summary>
    /// Write-access property to set arc at which object moves in
    /// </summary>
    public float ForceAngleArc
    {
        set { forceAngleArc = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // retrieve object's rigidbody 
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // find angle to target
        float deltaX = target.position.x - transform.position.x;
        float deltaY = target.position.y - transform.position.y;
        float angleToTarget = Mathf.Atan2(deltaY, deltaX);

        // apply randomized offset to angle within specified arc
        float halfArc = (forceAngleArc * Mathf.Deg2Rad) / 2;
        float randOffset = Random.Range(-halfArc, halfArc);
        angleToTarget += randOffset;

        // apply impulse force towards offset angle to target
        Vector2 forceDirection = new Vector2(Mathf.Cos(angleToTarget), Mathf.Sin(angleToTarget));
        myRigidbody2D.AddForce(forceDirection * impulseForceScale, ForceMode2D.Impulse);
    }

    // Called when object collides with another
    void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy self
        Destroy(gameObject);
    }
}
