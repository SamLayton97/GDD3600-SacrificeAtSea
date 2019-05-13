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
    bool isActive = false;

    // audio support variables
    [SerializeField] AudioSource alarmAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to proximity alarm event
        EventManager.AddProximityAlarmListener(ToggleAlarm);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Enables/disables proximity alarm
    /// </summary>
    /// <param name="activate">whether alarm is now active
    /// or inactive</param>
    void ToggleAlarm(bool activate)
    {
        Debug.Log("ACTIVATE: " + activate);

        // if alarm should activate and isn't currently active
        if (!isActive && activate)
        {
            // set alarm to active
            isActive = true;

            // sound alarm if alarm isn't already playing
            if (!alarmAudioSource.isPlaying)
                alarmAudioSource.Play();
        }

        // if alarm should deactivate and is currently active
        if (isActive && !activate)
        {
            // set alarm to inactive
            isActive = false;

            // stop alarm from sounding
            alarmAudioSource.Stop();
        }
    }
}
