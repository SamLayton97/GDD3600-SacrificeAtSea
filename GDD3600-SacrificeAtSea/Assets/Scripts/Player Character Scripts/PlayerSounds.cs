using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays sounds relevant to player-character
/// </summary>
public class PlayerSounds : MonoBehaviour
{
    // Audio source support
    [SerializeField] AudioSource myJumpingAudioSource;
    [SerializeField] AudioSource myLandingAudioSource;

    // Player-character audio clips
    [SerializeField] AudioClip[] jumpingAudioClips;
    [SerializeField] AudioClip[] landingAudioClips;

    /// <summary>
    /// Plays random jump sound effect from array of 
    /// "jump" audio clips.
    /// </summary>
    public void PlayJumpSound()
    {
        myJumpingAudioSource.PlayOneShot(jumpingAudioClips[Random.Range((int)0, (int)jumpingAudioClips.Length)]);
    }

    /// <summary>
    /// Plays random landing sound effect from array
    /// of "landing" audio clips.
    /// </summary>
    public void PlayLandingSound()
    {
        // if no sounds are already playing from audio source, play random sound
        if (!myLandingAudioSource.isPlaying)
            myLandingAudioSource.PlayOneShot(landingAudioClips[Random.Range((int)0, (int)landingAudioClips.Length)]);
    }
}
