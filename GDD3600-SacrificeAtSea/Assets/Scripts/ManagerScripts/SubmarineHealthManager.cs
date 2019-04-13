using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to sub collision event
        EventManager.AddSubmarineCollisionListener(HandleSubmarineCollision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handles manager-side consequences of a submarine collision
    void HandleSubmarineCollision()
    {
        // decrement health
        currHealth -= damagePerCollision;

        // TODO: if health drops below 0, Game Over
        if (currHealth <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }
}
