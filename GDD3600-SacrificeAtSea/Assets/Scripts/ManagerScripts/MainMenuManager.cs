﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager that facilitates scene movement within main menu
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // Handles when player clicks 'play' button
    public void HandlePlayButtonClickEvent()
    {
        // goes to submarine/gameplay scene
        SceneManager.LoadScene("SubmarineScene");
    }

    // Handles when player clicks 'quit' button
    public void HandleQuitButtonClickEvent()
    {
        // exit application
        Application.Quit();
        Debug.Log("Application quit.");
    }
}
