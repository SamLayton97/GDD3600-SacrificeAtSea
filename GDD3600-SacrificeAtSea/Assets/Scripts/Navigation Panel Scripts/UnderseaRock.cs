using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script controlling force, sprite, and rotation of undersea rock.
/// </summary>
public class UnderseaRock : MonoBehaviour
{
    // sprite swapping support
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite[] underseaRockSprites;

    // rotation support
    Rigidbody2D myRigidbody2D;
    [SerializeField] float maxRotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // retrieve references to various components
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // randomize undersea rock's sprite
        int randSpriteIndex = Random.Range((int)0, (int)underseaRockSprites.Length);
        mySpriteRenderer.sprite = underseaRockSprites[randSpriteIndex];

        // apply random rotation to rock
        float randRotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        myRigidbody2D.AddTorque(randRotationSpeed);
        
    }

    // Called when object collides with another
    void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy self
        Destroy(gameObject);
    }
}
