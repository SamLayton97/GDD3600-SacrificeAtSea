using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls opacity of all obstacles on the navigation panel,
/// reducing their visibility if the sonar is not functioning.
/// </summary>
public class SonarPingResponse : MonoBehaviour
{
    // define visibility variables
    [SerializeField] float maxFunctioningOpacity;
    [SerializeField] float minFunctioningOpacity;
    [SerializeField] float maxMalfunctioningOpacity;
    [SerializeField] float minMalfuctioningOpacity;

    // pulse control
    [SerializeField] float pulseRate = 1;
    int pulseDirection = -1;
    float maxAlpha = 1;
    float minAlpha = 0f;
    float currAlpha = 1;
    float alphaChangePerFrame;

    // visibility support variables
    GameObject[] navPanelObstacles;
    bool isSonarFunctioning = true;

    // Start is called before the first frame update
    void Start()
    {
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
        // calculate current visibility of obstacles
        currAlpha += pulseDirection * Time.deltaTime * alphaChangePerFrame;

        // if current visibility exceeds max/min bounds, reverse pulse direction
        if (currAlpha >= maxAlpha || currAlpha <= minAlpha)
            pulseDirection *= -1;

        // retrieve all obstacles on the navigation panel
        navPanelObstacles = GameObject.FindGameObjectsWithTag("CavernObstacle");

        // for each obstacle
        for (int i = 0; i < navPanelObstacles.Length; i++)
        {
            // appply current visibility to current obstacle
            SpriteRenderer currSpriteRenderer = navPanelObstacles[i].GetComponent<SpriteRenderer>();
            Color tempColor = currSpriteRenderer.color;
            tempColor.a = currAlpha;
            currSpriteRenderer.color = tempColor;
        }

        Debug.Log(currAlpha);
    }

    /// <summary>
    /// Updates sonar pulse peaks and valleys if sonar was updated
    /// </summary>
    /// <param name="updatedPart">submarine part with updated functionality</param>
    /// <param name="isNowFunctioning">whether updated part functions or not</param>
    void UpdateSonarFunctionality(SubmarineParts updatedPart, bool isNowFunctioning)
    {
        // if updated part was the ship's sonar (otherwise, disregard event)
        if (updatedPart == SubmarineParts.sonar)
        {
            // if sonar is now functioning
            if (isNowFunctioning)
            {
                // set max/min alpha to their normal values
                maxAlpha = maxFunctioningOpacity;
                minAlpha = minFunctioningOpacity;
            }
            // otherwise (sonar has now malfunctioned)
            else
            {
                // set max/min alpha to their reduced values
                maxAlpha = maxMalfunctioningOpacity;
                minAlpha = minMalfuctioningOpacity;
            }

            // update alpha change per frame
            alphaChangePerFrame = pulseRate * (maxAlpha - minAlpha);
        }
    }
}
