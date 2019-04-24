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

    void Awake()
    {
        // set up audio references
        myAudioSource = GetComponent<AudioSource>();
    }
}
