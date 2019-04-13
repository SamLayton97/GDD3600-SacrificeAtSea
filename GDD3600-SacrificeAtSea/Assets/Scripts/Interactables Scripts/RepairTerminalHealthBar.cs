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
    [SerializeField] Image healthBarFill;
    [SerializeField] DegradingPart degradingPart;
    float currHealth = 0;       // current health value
    float prevHealth = 100;     // health value of previous frame; used to determine whether part is under repair or not

    // health bar color support fields
    Color functioningColor = Color.cyan;
    Color brokenColor = Color.red;
    Color repairColor = Color.yellow;
    float functionalityThreshold = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        // read functionality threshold from degrading part script
        functionalityThreshold = degradingPart.FunctionalityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        // update scale of health bar 
        currHealth = degradingPart.CurrentHealth;
        healthSlider.value = currHealth;

        // if health value has grown (player is repairing it), set color to repair color
        if (currHealth > prevHealth)
        {
            healthBarFill.color = Color.yellow;
        }
        // if health value is over threshold, set color to functioning
        else if (currHealth >= functionalityThreshold)
        {
            healthBarFill.color = functioningColor;
        }
        // otherwise (broken part and not being repaired), set color to broken color
        else
        {
            healthBarFill.color = Color.red;
        }

        // update previous health value
        prevHealth = currHealth;
    }
}
