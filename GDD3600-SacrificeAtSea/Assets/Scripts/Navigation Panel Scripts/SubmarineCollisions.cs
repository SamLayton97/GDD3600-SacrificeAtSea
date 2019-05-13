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
    ProximityAlarmEvent proximityAlarmEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of Submarine Collision event
        submarineCollisionEvent = new SubmarineCollisionEvent();
        EventManager.AddSubmarineCollisionInvoker(this);

        // add self as invoker of Collect Treasure event
        collectTreasureEvent = new CollectTreasureEvent();
        EventManager.AddCollectTreasureInvoker(this);

        // add self as invoker of Proximity Alarm event
        proximityAlarmEvent = new ProximityAlarmEvent();
        EventManager.AddProximityAlarmInvoker(this);

    }

    // Called first frame submarine enters collision with another
    void OnCollisionEnter2D(Collision2D collision)
    {
        // retrieve tag of other object in collision
        string collisionTag = collision.gameObject.tag;

        // if collision object was tagged as an obstacle
        if (collisionTag == "CavernObstacle")
        {
            // invoke submarine collision event
            submarineCollisionEvent.Invoke();
        }
        // if collision object was tagged as treasure
        else if (collisionTag == "CavernTreasure")
        {
            // invoke collect treasure event
            collectTreasureEvent.Invoke();
        }
    }

    // Called first frame object enters proximity of submarine
    void OnTriggerEnter2D(Collider2D collision)
    {
        // activate submarine's proximity alarm
        Debug.Log("ENTER PROXIMITY");
        proximityAlarmEvent.Invoke(true);
    }

    // Called when object leaves proximity of submarine
    void OnTriggerExit2D(Collider2D collision)
    {
        // deactivate submarine's proximity alarm
        Debug.Log("EXIT PROXIMITY");
        proximityAlarmEvent.Invoke(false);
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

    /// <summary>
    /// Adds listener to object's proximity alarm event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddProximityAlarmListener(UnityAction<bool> newListener)
    {
        proximityAlarmEvent.AddListener(newListener);
    }

}
