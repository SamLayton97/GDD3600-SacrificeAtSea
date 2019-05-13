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

    // proximity alarm support
    bool isAlarmColliderEmpty = true;       // whether proximity alarm collider is clear of obstacles
    bool wasAlarmColliderEmpty = true;      // whether collider was clear last frame -- used for updates

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

    // Called once per frame
    void Update()
    {
        // if collider was empty but now is not, sound alarm
        if (!isAlarmColliderEmpty && wasAlarmColliderEmpty)
            proximityAlarmEvent.Invoke(true);
        // if collider is empty but was not, stop alarm
        if (isAlarmColliderEmpty && !wasAlarmColliderEmpty)
            proximityAlarmEvent.Invoke(false);
    }

    // Called after Update ends
    void LateUpdate()
    {
        // update previous frame collider-empty flag
        wasAlarmColliderEmpty = isAlarmColliderEmpty;
    }

    // Called fixed number of timer per second
    void FixedUpdate()
    {
        // reset collider-empty flag
        isAlarmColliderEmpty = true;
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

    // Called each frame an object is within submarine's proximity collider
    void OnTriggerStay2D(Collider2D collision)
    {
        isAlarmColliderEmpty = false;
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
