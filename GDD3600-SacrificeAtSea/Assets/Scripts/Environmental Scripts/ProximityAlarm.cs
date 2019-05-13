using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles audio-visual proximity alarm triggered when object
/// approaches submarine on the nav panel.
/// </summary>
public class ProximityAlarm : MonoBehaviour
{
    // alarm activity variables
    bool isCurrentlyActive = true;

    // audio support variables
    [SerializeField] AudioSource alarmAudioSource;
    [SerializeField] float alarmDelay = 1.5f;
    float alarmTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to proximity alarm event
        EventManager.AddProximityAlarmListener(ToggleAlarm);
    }

    // Update is called once per frame
    void Update()
    {
        // if alarm is currently active
        if (isCurrentlyActive)
        {
            // if alarm timer bottoms out
            if (alarmTimer <= 0)
            {
                // sound alarm and reset timer
                alarmAudioSource.Play();
                alarmTimer = alarmDelay;
            }

            // decrement time to next alarm
            alarmTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Enables/disables proximity alarm
    /// </summary>
    /// <param name="isActive">whether alarm is now active
    /// or inactive</param>
    void ToggleAlarm(bool isActive)
    {
        
    }
}
