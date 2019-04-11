﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A submarine part that degrades over time
/// </summary>
public class DegradingPart : MonoBehaviour
{
    #region Fields

    // serialized variables
    [SerializeField] float degredationRate = 0.01f;
    [SerializeField] float repairRate = 5;
    [SerializeField] SubmarineParts correspondingPart = SubmarineParts.ballastTanks;
    [SerializeField] float functionalityThreshold = 30;

    // health variables
    const float MaxHealth = 100;
    float currHealth = MaxHealth;

    // degredation support variables
    float degredationTimer = 0;

    // repair support variables
    bool playerIsColliding = false;

    // update part-functionality event support
    UpdateFunctionalityEvent updateFunctionalityEvent;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // calculate time to degrade
        degredationTimer = 1 / degredationRate;

        // add self as invoker of Update Functionality event
        updateFunctionalityEvent = new UpdateFunctionalityEvent();
        EventManager.AddUpdateFunctionalityInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        // retrieve interact input
        float interactInput = Input.GetAxisRaw("Interact");

        // if player is colliding with part and interacting with it, increment health
        if (playerIsColliding && interactInput != 0)
        {
            currHealth = Mathf.Min(MaxHealth, currHealth + (Time.deltaTime * repairRate)); 
        }
        // otherwise, decrement health
        else
        {
            currHealth = Mathf.Max(0, currHealth - (Time.deltaTime * degredationRate));
        }
    }

    // Called on first frame part is in collision with another
    void OnTriggerEnter2D(Collider2D collision)
    {
        // set player-collision to true
        playerIsColliding = true;
    }

    // Called on frame part leaves collision with another
    void OnTriggerExit2D(Collider2D collision)
    {
        // set player collision to false
        playerIsColliding = false;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds given listener for Update Functionality event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddUpdateFunctionalityListener(UnityAction<SubmarineParts, bool> listener)
    {
        updateFunctionalityEvent.AddListener(listener);
    }

    #endregion
}
