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

    // ping speed control


    // visibility support variables
    SpriteRenderer mySpriteRenderer;
    bool isSonarFunctioning = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
