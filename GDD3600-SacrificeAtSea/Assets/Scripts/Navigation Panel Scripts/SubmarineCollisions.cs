using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script to facilitate collisions between submarine
/// and obstacles/treasure on navigation panel.
/// </summary>
public class SubmarineCollisions : MonoBehaviour
{
    // event support
    SubmarineCollisionEvent submarineCollisionEvent;
    CollectTreasureEvent collectTreasureEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of Submarine Collision event
        submarineCollisionEvent = new SubmarineCollisionEvent();
        EventManager.AddSubmarineCollisionInvoker(this);

        // TODO: add self as invoker of Collect Treasure event
        collectTreasureEvent = new CollectTreasureEvent();

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

    /// <summary>
    /// Adds listener to object's collect treasure event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddCollectTreasureListener(UnityAction newListener)
    {
        collectTreasureEvent.AddListener(newListener);
    }

}
