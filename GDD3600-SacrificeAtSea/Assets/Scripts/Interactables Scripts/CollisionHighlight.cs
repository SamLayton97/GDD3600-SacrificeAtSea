using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls highlight to indicate player is colliding
/// with an interactable object.
/// </summary>
public class CollisionHighlight : MonoBehaviour
{
    // highlight control
    [SerializeField] SpriteRenderer highlightRenderer;
    Color transparent;
    Color opaque;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // initialize preset color variables
        opaque = highlightRenderer.color;
        transparent = highlightRenderer.color;
        transparent.a = 0;

        // initialize highlight sprite to completely transparent
        highlightRenderer.color = transparent;
    }

    /// <summary>
    /// Called when player enters collision with interactable object.
    /// </summary>
    /// <param name="collision">collision info</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // set highlight sprite to completely transparent
        highlightRenderer.color = opaque;
    }

    /// <summary>
    /// Called when player exits collision with interactable object.
    /// </summary>
    /// <param name="collision">collision info</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        // set highlight sprite to completely visible
        highlightRenderer.color = transparent;
    }
}
