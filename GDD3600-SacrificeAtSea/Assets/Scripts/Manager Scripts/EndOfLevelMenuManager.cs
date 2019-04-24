using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager that facilitates scene movement within end of level menu
/// </summary>
public class EndOfLevelMenuManager : MonoBehaviour
{
    // Handles when player clicks 'Replay' button
    public void HandleReplayButtonClickEvent()
    {
        // go to submarine/gameplay scene
        SceneManager.LoadScene("SubmarineScene");
    }

    // Handles when player clicks 'Main Menu' button
    public void HandleMainMenuButtonClickEvent()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
