using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Determines whether player has succeeded in dodging
/// this tutorial mine (and its volley).
/// </summary>
public class DodgedTutorialMine : MonoBehaviour
{
    // event support
    DodgedVolleyEvent dodgedVolleyEvent;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: set self as invoker of Dodged Volley event
        dodgedVolleyEvent = new DodgedVolleyEvent();
    }

    /// <summary>
    /// Called when object collides with another
    /// </summary>
    /// <param name="collision">collision info</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if object isn't tagged as player's submarine
        if (!collision.gameObject.CompareTag("SubmarineIcon"))
        {
            // invoke dodged volley event
            dodgedVolleyEvent.Invoke();
        }
    }

    /// <summary>
    /// Adds given method as listener to object's dodged volley event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddDodgedVolleyListener(UnityAction newListener)
    {
        dodgedVolleyEvent.AddListener(newListener);
    }
}
