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

    // Start is called before the first frame update
    void Start()
    {
        // randomize undersea rock's sprite
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        int randSpriteIndex = Random.Range((int)0, (int)underseaRockSprites.Length);
        mySpriteRenderer.sprite = underseaRockSprites[randSpriteIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
