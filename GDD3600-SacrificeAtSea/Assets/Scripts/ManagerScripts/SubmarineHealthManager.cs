using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script managing submarine's overall health/hull integrity
/// </summary>
public class SubmarineHealthManager : MonoBehaviour
{
    // health variables
    const int MaxHealth = 100;
    int currHealth = 100;

    // damage variables
    [SerializeField] int damagePerCollision = 15;

    // event support
    UpdateHullIntegrityEvent updateHullIntegrityEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of update hull integrity event
        updateHullIntegrityEvent = new UpdateHullIntegrityEvent();
        EventManager.AddHullIntegrityInvoker(this);

        // add self as listener to sub collision event
        EventManager.AddSubmarineCollisionListener(HandleSubmarineCollision);
    }

    // Handles manager-side consequences of a submarine collision
    void HandleSubmarineCollision()
    {
        // decrement health and send out updated hull integrity
        currHealth -= damagePerCollision;
        updateHullIntegrityEvent.Invoke(currHealth);

        // TODO: if health drops below 0, Game Over
        if (currHealth <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }

    // Adds listener to object's hull integrity event
    public void AddUpdateHullIntegrityListener(UnityAction<int> newListener)
    {
        updateHullIntegrityEvent.AddListener(newListener);
    }
}
