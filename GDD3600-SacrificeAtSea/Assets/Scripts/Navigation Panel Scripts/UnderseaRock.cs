using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script controlling collision detection undersea obstacle (seamine or wall).
/// </summary>
public class UnderseaRock : MonoBehaviour
{
    // Called when object collides with another
    void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy self
        Destroy(gameObject);
    }
}
