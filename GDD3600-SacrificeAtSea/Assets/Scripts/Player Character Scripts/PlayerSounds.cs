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
    [SerializeField] AudioSource myFootstepsAudioSource;

    // Player-character audio clips
    [SerializeField] AudioClip[] jumpingAudioClips;
    [SerializeField] AudioClip[] landingAudioClips;
    [SerializeField] AudioClip[] footstepAudioClips;

    // Footstep sound effect support
    [SerializeField] float timeBetweenFootsteps = 1f;

    private bool isWalking = false;
    float footstepCounter = 0f;

    /// <summary>
    /// Write-access property showing whether player-character is walking
    /// </summary>
    public bool IsWalking
    {
        set { isWalking = value; }
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // if player-character is walking
        if (isWalking)
        {
            // if footstep sound counter has hit zero
            if (footstepCounter <= 0)
            {
                // play footstep sound effect
                PlayFootstepSound();

                // reset counter
                footstepCounter = timeBetweenFootsteps;
            }
            // otherwise, decrement counter
            else
                footstepCounter -= Time.deltaTime;

        }
        // otherwise (i.e., player isn't walking), ready counter to play footstep when player is walking again
        else
            footstepCounter = 0;
    }

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

    /// <summary>
    /// Plays random footstep sound effect from array
    /// of "footstep" audio clips.
    /// </summary>
    void PlayFootstepSound()
    {
        myFootstepsAudioSource.PlayOneShot(footstepAudioClips[Random.Range((int)0, (int)footstepAudioClips.Length)]);
    }
}
