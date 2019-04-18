using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Causes nav panel object to pulsate and appear difficult to see
/// when submarine's sonar is malfunctioning.
/// </summary>
public class SonarPingResponse : MonoBehaviour
{
    // define visibility variables
    [SerializeField] float maxFunctioningOpacity;
    [SerializeField] float minFunctioningOpacity;
    [SerializeField] float maxMalfunctioningOpacity;
    [SerializeField] float minMalfuctioningOpacity;

    // pulse control
    [SerializeField] float pulseRate;
    int pulseDirection = -1;
    float maxAlpha = 1;
    float minAlpha = 0;
    float alphaChangePerFrame;

    // visibility support variables
    SpriteRenderer mySpriteRenderer;
    bool isSonarFunctioning = true;

    // Start is called before the first frame update
    void Start()
    {
        // retrieve object's sprite renderer
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // add self as listener to update functionality event
        EventManager.AddUpdateFunctionalityListeners(UpdateSonarFunctionality);

        // initialize starting max/min alpha and calculate alpha change per frame
        //maxAlpha = maxFunctioningOpacity;
        //minAlpha = minFunctioningOpacity;
        alphaChangePerFrame = pulseRate * (maxAlpha - minAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        // increment/decrement object's sprite alpha by pulse rate
        Color tempColor = mySpriteRenderer.color;
        tempColor.a += pulseDirection * Time.deltaTime * alphaChangePerFrame;

        // if alpha value exceeds max or min bounds, reverse its change direction
        if (tempColor.a <= minAlpha || tempColor.a >= maxAlpha)
            pulseDirection *= -1;

        Debug.Log(tempColor.a);

        // apply new alpha value to sprite
        mySpriteRenderer.color = tempColor;
    }

    /// <summary>
    /// Updates sonar pulse peaks and valleys if sonar was updated
    /// </summary>
    /// <param name="updatedPart">submarine part with updated functionality</param>
    /// <param name="isNowFunctioning">whether updated part functions or not</param>
    void UpdateSonarFunctionality(SubmarineParts updatedPart, bool isNowFunctioning)
    {
        //// if updated part was the ship's sonar (otherwise, disregard event)
        //if (updatedPart == SubmarineParts.sonar)
        //{
        //    // if sonar is now functioning
        //    if (isNowFunctioning)
        //    {
        //        // set max/min alpha to their normal values
        //        maxAlpha = maxFunctioningOpacity;
        //        minAlpha = minFunctioningOpacity;
        //    }
        //    // otherwise (sonar has now malfunctioned)
        //    else
        //    {
        //        // set max/min alpha to their reduced values
        //        maxAlpha = maxMalfunctioningOpacity;
        //        minAlpha = minMalfuctioningOpacity;
        //    }

        //    // update alpha change per frame
        //    alphaChangePerFrame = pulseRate * (maxAlpha - minAlpha);
        //}
    }
}
