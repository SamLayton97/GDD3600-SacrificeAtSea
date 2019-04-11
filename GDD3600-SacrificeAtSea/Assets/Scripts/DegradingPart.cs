using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A submarine part that degrades over time
/// </summary>
public class DegradingPart : MonoBehaviour
{

    // public variables
    [SerializeField] float degredationRate = 1;
    [SerializeField] float repairRate = 5;

    // health variables
    const int MaxHealth = 100;
    int currHealth = MaxHealth;

    // timer support variables
    float degredationCounter = 0;
    float degredationTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // calculate time to degrade
        degredationTimer = 1 / degredationRate;
    }

    // Update is called once per frame
    void Update()
    {
        // if degredation counter has exceeded timer and part's health is not 0
        if (degredationCounter >= degredationTimer && currHealth > 0)
        {
            // reset counter
            degredationCounter = 0;

            // decrement submarine part's health if not already 0
            currHealth = Mathf.Max(0, currHealth - 1);

            // FOR DEGUBBING: print current health of part
            Debug.Log("Current Health: " + currHealth);
        }

        // increment counter by time between frames
        degredationCounter += Time.deltaTime;
    }
}
