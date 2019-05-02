using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Facilitates scene movement from Controls page
/// </summary>
public class ControlsMenuManager : MonoBehaviour
{
    // scene movement support varables
    [SerializeField] string titleScreenSceneName = "";

    // Handles when user clicks "Back" button
    public void HandleBackButtonClickEvent()
    {
        // return to title screen
        SceneManager.LoadScene(titleScreenSceneName);
    }
}
