using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls health bar on canvas local to game object
/// </summary>
public class RepairTerminalHealthBar : MonoBehaviour
{
    // health bar support fields
    [SerializeField] Slider healthSlider;
    [SerializeField] DegradingPart degradingPart;

    // health bar color support fields
    //Color functioningColor = new Color(0, 169, 255);    // sky blue

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // update scale of health bar 
        healthSlider.value = degradingPart.CurrentHealth;
    }
}
