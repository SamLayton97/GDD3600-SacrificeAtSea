using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages pausing of game
/// </summary>
public class PauseManager : MonoBehaviour
{
    // pause menu support variables
    [SerializeField] GameObject pauseMenu;

    /// <summary>
    /// Property returning whether game is currently paused
    /// </summary>
    public bool IsGamePaused
    {
        get
        {
            // if time scale is 0, return true
            if (Time.timeScale == 0)
                return true;
            // otherwise, return false
            else
                return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if user enters pause input and game is not already paused
        if (Input.GetAxisRaw("Pause") != 0 && !IsGamePaused)
        {
            // create instance of pause menu
            Instantiate(pauseMenu);
        }
    }
}
