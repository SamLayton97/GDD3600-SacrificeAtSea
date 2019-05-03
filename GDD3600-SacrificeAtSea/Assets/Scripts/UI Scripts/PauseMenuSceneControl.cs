using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script controlling pause menu's scene navigation
/// </summary>
public class PauseMenuSceneControl : MonoBehaviour
{
    // Relevant scene variables
    [SerializeField] string menuSceneName;
    [SerializeField] string tutorialSceneName;

    // Start is called before the first frame update
    void Start()
    {
        // pause game
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resumes game when user clicks 'Resume' button
    /// </summary>
    public void HandleResumeGameButton()
    {
        // unpause game
        Time.timeScale = 1;

        // remove pause menu
        Destroy(gameObject);
    }

    /// <summary>
    /// Restarts current level when user clicks 'Restart' button
    /// </summary>
    public void HandleRestartLevelButton()
    {
        // unpause game
        Time.timeScale = 1;

        // reload current scene
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.name);
    }

    /// <summary>
    /// Returns to tutorial when user clicks 'Tutorial' button
    /// </summary>
    public void HandleReturnToTutorialButton()
    {
        // unpause game
        Time.timeScale = 1;

        // load tutorial scene
        SceneManager.LoadScene(tutorialSceneName);
    }

    /// <summary>
    /// Exits to menu when user clicks 'Quit' button
    /// </summary>
    public void HandleReturnToMenuButton()
    {
        // unpause game
        Time.timeScale = 1;

        // exit to main menu
        SceneManager.LoadScene(menuSceneName);
    }
}
