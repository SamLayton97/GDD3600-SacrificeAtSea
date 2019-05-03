using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays message and advances tutorial upon collision
/// </summary>
public class MessageTerminal : MonoBehaviour
{
    // tutorial sequencing support
    [SerializeField] TutorialStages terminalTriggers = TutorialStages.InitialSteps;
    bool messageHeard = false;

    // blinking light support
    [SerializeField] SpriteRenderer blinkingLight;
    [SerializeField] float blinkRate = 2f;
    float blinkCounter = 0;

    // Start is called before the first frame update
    void Start()
    {

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
        // set message as heard
        messageHeard = true;

        // stop light from flashing
        Color endColor = blinkingLight.color;
        endColor.a = 0;
        blinkingLight.color = endColor;
    }
}
