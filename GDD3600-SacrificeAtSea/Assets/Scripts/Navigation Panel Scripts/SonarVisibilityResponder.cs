using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sets the alpha value of a navigation panal obstacle
/// to match that of all other obstacles.
/// </summary>
public class SonarVisibilityResponder : MonoBehaviour
{
    // visibility support variables
    SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // retrieves sprite renderer component
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // add self as listener to update obstacle visibility event
        EventManager.AddUpdateVisibilityListener(UpdateObstacleVisibility);
    }
    
    /// <summary>
    /// Updated visibility of obstacle on navigation panel
    /// </summary>
    /// <param name="newAlpha"></param>
    void UpdateObstacleVisibility(float newAlpha)
    {
        Debug.Log("updated");

        // set alpha value of this object's sprite to sonar controller's value
        Color tempColor = mySpriteRenderer.color;
        tempColor.a = newAlpha;
        mySpriteRenderer.color = tempColor;
    }
}
