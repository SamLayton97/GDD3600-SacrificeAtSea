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
    [SerializeField] float alarmDelay = 2.0f;
    float alarmTimer = 0;

    // visual support variables
    [SerializeField] GameObject[] overheadAlarms;
    [SerializeField] Sprite activeAlarmSprite;
    [SerializeField] Sprite inactiveAlarmSprite;

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to proximity alarm event
        EventManager.AddProximityAlarmListener(ToggleAlarm);
    }

    // Update is called once per frame
    void Update()
    {
        // if proximity alarm is active
        if (isActive)
        {
            // if alarm timer bottoms out
            if (alarmTimer <= 0)
            {
                // sound alarm and reset timer
                alarmAudioSource.Play();
                alarmTimer = alarmDelay;
            }

            // decrement time to next alarm sounding
            alarmTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Enables/disables proximity alarm
    /// </summary>
    /// <param name="activate">whether alarm is now active
    /// or inactive</param>
    void ToggleAlarm(bool activate)
    {
        // if alarm should activate and isn't currently active
        if (!isActive && activate)
        {
            // set alarm to active
            isActive = true;

            // set sprites of overhead alarms to active variant
            for (int i = 0; i < overheadAlarms.Length; i++)
                overheadAlarms[i].GetComponent<SpriteRenderer>().sprite = activeAlarmSprite;

            // start alarm
            alarmTimer = 0;
        }

        // if alarm should deactivate and is currently active
        if (isActive && !activate)
        {
            // set alarm to inactive
            isActive = false;

            // set sprites of overhead alarms to inactive variant
            for (int i = 0; i < overheadAlarms.Length; i++)
                overheadAlarms[i].GetComponent<SpriteRenderer>().sprite = inactiveAlarmSprite;
        }
    }
}
