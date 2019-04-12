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

    // force support
    Rigidbody2D myRigidbody2D;
    [SerializeField] float impulseForceScale = 1f;
    [SerializeField] float forceAngleArc = 35f;
    Transform target;

    /// <summary>
    /// Property with write access containing target's transform info.
    /// </summary>
    public Transform Target
    {
        set { target = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // retrieve references to various components
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // randomize undersea rock's sprite
        int randSpriteIndex = Random.Range((int)0, (int)underseaRockSprites.Length);
        mySpriteRenderer.sprite = underseaRockSprites[randSpriteIndex];

        // find angle to target
        float deltaX = target.position.x - transform.position.x;
        float deltaY = target.position.y - transform.position.y;
        float angleToTarget = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        // apply randomized offset to angle within specified arc
        float halfArc = forceAngleArc / 2;
        float randOffset = Random.Range(-halfArc, halfArc);
        angleToTarget += randOffset;

        // TODO: apply impulse force in general direction of player's submarine
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
