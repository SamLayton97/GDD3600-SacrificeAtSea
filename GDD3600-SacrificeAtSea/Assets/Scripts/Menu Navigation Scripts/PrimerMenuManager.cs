using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Facilitates scene movement from "Thematic Primer" menu.
/// </summary>
public class PrimerMenuManager : MonoBehaviour
{
    // scene transition support variables
    [SerializeField] string fromPlaySceneName = "";
    [SerializeField] string fromBackSceneName = "";

    // Handles when player clicks "play" button
    public void HandlePlayButtonOnClick()
    {
        // go to first gameplay scene
        SceneManager.LoadScene(fromPlaySceneName);
    }

    // Handles when player clicks 'back' button
    public void HandleBackButtonOnClick()
    {
        // return to title screen
        SceneManager.LoadScene(fromBackSceneName);
    }
}
