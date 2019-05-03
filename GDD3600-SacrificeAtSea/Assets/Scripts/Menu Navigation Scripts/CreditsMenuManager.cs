using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Facilitates scene movement from credits menu
/// </summary>
public class CreditsMenuManager : MonoBehaviour
{
    // scene transition support variables
    [SerializeField] string fromBackSceneName = "";

    // Handles when player clicks 'back' button
    public void HandleBackButtonOnClick()
    {
        // return to title screen
        SceneManager.LoadScene(fromBackSceneName);
    }
}
