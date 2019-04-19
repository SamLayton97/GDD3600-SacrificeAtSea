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

    /// <summary>
    /// Plays audio-visual feedback for when player interacts with un-interactable object
    /// </summary>
    public void PlayInteractErrorFeedback()
    {
        // if not already playing, play error sound effect
        if (!errorAudioSource.isPlaying)
            errorAudioSource.Play();
    }
}
