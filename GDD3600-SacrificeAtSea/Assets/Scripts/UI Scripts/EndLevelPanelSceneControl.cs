using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script controlling end-of-level panel's scena navigation
/// </summary>
public class EndLevelPanelSceneControl : MonoBehaviour
{
    // Relevant scenes variables
    [SerializeField] string titleMenuSceneName;
    [SerializeField] string nextLevelSceneName;

    /// <summary>
    /// Handles when player clicks on next level button
    /// </summary>
    public void HandleNextLevelButton()
    {
        // go to next level in game
        SceneManager.LoadScene(nextLevelSceneName);
    }

    /// <summary>
    /// Handles when player clicks on retry level button
    /// </summary>
    public void HandleRetryLevelButton()
    {
        // reload current scene
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.name);
    }

    /// <summary>
    /// Handles when player clicks on return to menu button
    /// </summary>
    public void HandleReturnToMenuButton()
    {
        // go to title menu
        SceneManager.LoadScene(titleMenuSceneName);
    }
}
