using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Facilitates scene movement within main menu
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // scene transition support variables
    [SerializeField] string fromPlaySceneName = "";
    [SerializeField] string fromHelpSceneName = "";
    [SerializeField] string fromCreditsSceneName = "";

    // Handles when player clicks 'play' button
    public void HandlePlayButtonClickEvent()
    {
        // goes to first submarine/gameplay scene
        SceneManager.LoadScene(fromPlaySceneName);
    }

    // Handles when player clicks "help" button
    public void HandleHelpButtonClickEvent()
    {
        // goes to controls page
        SceneManager.LoadScene(fromHelpSceneName);
    }

    // Handles when player clicks "credits" button
    public void HandleCreditsButtonClickEvent()
    {
        // goes to credits page
        SceneManager.LoadScene(fromCreditsSceneName);
    }

    // Handles when player clicks 'quit' button
    public void HandleQuitButtonClickEvent()
    {
        // exit application
        Application.Quit();
        Debug.Log("Application quit.");
    }
}
