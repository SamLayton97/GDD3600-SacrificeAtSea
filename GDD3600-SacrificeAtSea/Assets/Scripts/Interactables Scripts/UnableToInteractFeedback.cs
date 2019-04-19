using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides audio-visual feedback for objects player is unable to interact with
/// </summary>
public class UnableToInteractFeedback : MonoBehaviour
{
    // audio feedback variables
    [SerializeField] AudioSource errorAudioSource;

    // particle feedback
    [SerializeField] GameObject malfunctioningControlsEffect;

    /// <summary>
    /// Plays audio-visual feedback for when player interacts with un-interactable object
    /// </summary>
    public void PlayInteractErrorFeedback()
    {
        // if sound effect is not already playing (prevents spamming)
        if (!errorAudioSource.isPlaying)
        {
            // play error sound
            errorAudioSource.Play();

            // create particle effect at object's location
            Instantiate(malfunctioningControlsEffect, transform);
        }

    }
}
