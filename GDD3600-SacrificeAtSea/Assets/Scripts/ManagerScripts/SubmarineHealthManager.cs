using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using EZCameraShake;

/// <summary>
/// Script managing submarine's overall health/hull integrity
/// </summary>
public class SubmarineHealthManager : MonoBehaviour
{
    // health variables
    const int MaxHealth = 100;
    int currHealth = 100;

    // damage variables
    [SerializeField] int damagePerCollision = 15;

    // event support
    UpdateHullIntegrityEvent updateHullIntegrityEvent;

    // screen shake support variables
    [SerializeField] float shakeMagnitude = 4f;
    [SerializeField] float shakeRoughness = 4f;
    [SerializeField] float shakeFadeInTime = .1f;
    [SerializeField] float shakeFadeOutTime = 1f;

    // sound effect support variables
    [SerializeField] AudioSource subCollisionsAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of update hull integrity event
        updateHullIntegrityEvent = new UpdateHullIntegrityEvent();
        EventManager.AddHullIntegrityInvoker(this);

        // add self as listener to sub collision event
        EventManager.AddSubmarineCollisionListener(HandleSubmarineCollision);
    }

    // Handles manager-side consequences of a submarine collision
    void HandleSubmarineCollision()
    {
        // decrement health and send out updated hull integrity (locked to 0 or greater)
        currHealth = Mathf.Max(currHealth - damagePerCollision, 0);
        updateHullIntegrityEvent.Invoke(currHealth);

        // shake screen
        CameraShaker.Instance.ShakeOnce(shakeMagnitude, shakeRoughness, shakeFadeInTime, shakeFadeOutTime);

        // play submarine collision sound effect
        subCollisionsAudioSource.Play();

        // if health drops below 0, Game Over
        if (currHealth <= 0)
        {
            // go to game over scene
            SceneManager.LoadScene("LevelFailedScene");
        }
    }

    // Adds listener to object's hull integrity event
    public void AddUpdateHullIntegrityListener(UnityAction<int> newListener)
    {
        updateHullIntegrityEvent.AddListener(newListener);
    }
}
