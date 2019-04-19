using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls particle effects that come from the repair terminal
/// </summary>
public class RepairTerminalParticles : MonoBehaviour
{
    // particle effects
    [SerializeField] GameObject repairInProgressEffect;
    [SerializeField] GameObject repairCompleteEffect;

    // particle system controller variables
    ParticleSystem inProgressParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        // retrieve reference to repair-in-progress particle system
        inProgressParticleSystem = repairInProgressEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Facilitates emission of 'repair-in-progress' effects
    /// </summary>
    /// <param name="isOn">whether particle system should turn or off</param>
    public void ToggleInProgressParticles(bool isOn)
    {
        //// if user wants to turn particle system on, turn them on
        if (isOn)
        {
            inProgressParticleSystem.Play();
        }
        // otherwise, pause them
        else
        {
            inProgressParticleSystem.Clear();
            inProgressParticleSystem.Pause();
        }
    }

    /// <summary>
    /// Facilitates emission of 'repair complete' effects
    /// </summary>
    public void SpawnRepairCompleteParticles()
    {
        // create instance of repair complete effect at object's position
        Instantiate(repairCompleteEffect, transform);
    }

}
