using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script to facilitate collisions between submarine
/// and obstacles on navigation panel.
/// </summary>
public class SubmarineCollisions : MonoBehaviour
{
    // event support
    SubmarineCollisionEvent submarineCollisionEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of Submarine Collision event
        submarineCollisionEvent = new SubmarineCollisionEvent();
        EventManager.AddSubmarineCollisionInvoker(this);
    }

    // Called first frame object enters collision with another
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if collision object was tagged as an obstacle
        if (collision.gameObject.CompareTag("CavernObstacle"))
        {
            // invoke submarine collision event
            submarineCollisionEvent.Invoke();
        }
    }

    // Adds listener to object's submarine collision event
    public void AddCollisionListener(UnityAction newListener)
    {
        submarineCollisionEvent.AddListener(newListener);
    }
}
