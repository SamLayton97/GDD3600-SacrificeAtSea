using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager that facilitates scene movement within main menu
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // Handles when player clicks 'play' button
    public void HandlePlayButtonClickEvent()
    {
        // goes to first submarine/gameplay scene
        SceneManager.LoadScene("EasySubmarineScene");
    }

    // Handles when player clicks 'quit' button
    public void HandleQuitButtonClickEvent()
    {
        // exit application
        Application.Quit();
        Debug.Log("Application quit.");
    }
}
