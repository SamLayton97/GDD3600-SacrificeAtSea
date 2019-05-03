using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Displays message and advances tutorial upon collision
/// </summary>
public class MessageTerminal : MonoBehaviour
{
    // tutorial sequencing support
    [SerializeField] TutorialStages terminalTriggers;   // tutorial stage that terminal triggers on initial collision with player
    bool messageHeard = false;
    TriggerNextStageEvent triggerNextStageEvent;

    // audio support
    [SerializeField] AudioSource messageReceivedAudioSource;

    // blinking light support
    [SerializeField] SpriteRenderer blinkingLight;
    [SerializeField] float blinkRate = 2f;
    float blinkCounter = 0;

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of trigger next stage event
        triggerNextStageEvent = new TriggerNextStageEvent();
        EventManager.AddTriggerNextStageInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        // if terminal's message hasn't been heard and blinking light counter exceeds rate
        if (!messageHeard && blinkCounter >= (1 / blinkRate))
        {
            // retrieve current color of blinking light
            Color newLightColor = blinkingLight.color;

            // calculate and apply new alpha of light
            newLightColor.a = 1 - newLightColor.a;
            blinkingLight.color = newLightColor;

            // reset counter
            blinkCounter = 0;
        }

        // increment counter
        blinkCounter += Time.deltaTime;
    }

    /// <summary>
    /// Called when another object collides with this object's
    /// trigger collision box.
    /// </summary>
    /// <param name="collision">collision info</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if message hasn't already been heard
        if (!messageHeard)
        {
            // set message as heard
            messageHeard = true;

            // stop light from flashing
            Color endColor = blinkingLight.color;
            endColor.a = 0;
            blinkingLight.color = endColor;
        }

        // invoke trigger next tutorial stage event
        triggerNextStageEvent.Invoke(terminalTriggers);

        // if not already playing, play message received sound effect
        if (!messageReceivedAudioSource.isPlaying)
            messageReceivedAudioSource.Play();
    }

    /// <summary>
    /// Called when another object exits collision with
    /// this one's trigger collision box.
    /// </summary>
    /// <param name="collision">collision info</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    #endregion

    public void AddTriggerNextStageListener(UnityAction<TutorialStages> newListener)
    {

    }

}
