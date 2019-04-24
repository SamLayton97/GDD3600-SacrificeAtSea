using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays sounds relevant to player-character
/// </summary>
public class PlayerSounds : MonoBehaviour
{
    // Audio source support
    AudioSource myAudioSource;

    // Player-character audio clips
    [SerializeField] AudioClip[] jumpingAudioClips;
    [SerializeField] AudioClip[] landingAudioClips;

    void Awake()
    {
        // set up audio references
        myAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays random jump sound effect from array of 
    /// "jump" audio clips.
    /// </summary>
    public void PlayJumpSound()
    {
        myAudioSource.PlayOneShot(jumpingAudioClips[Random.Range((int)0, (int)jumpingAudioClips.Length)]);
    }
}
