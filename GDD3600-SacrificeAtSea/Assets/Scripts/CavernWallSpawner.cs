using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that controls spawning of walls (ceiling/floor)
/// </summary>
public class CavernWallSpawner : MonoBehaviour
{
    // spawning support
    [SerializeField] GameObject cavernWall;
    [SerializeField] float spawnRate = 1;
    [SerializeField] float xSpawnOffset = 2.9f;
    [SerializeField] float ySpawnOffset = 1.5f;

    // timer support
    float nextSpawnCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if spawn counter exceeds limit
        if (nextSpawnCounter > spawnRate)
        {
            // reset counter
            nextSpawnCounter = 0;

            // TEMP CODE: spawn new cavern wall
            Vector3 spawnDisplacement = new Vector3(xSpawnOffset, ySpawnOffset);
            Instantiate(cavernWall, transform.position + spawnDisplacement, Quaternion.identity);
        }

        // increment counter by time between frames
        nextSpawnCounter += Time.deltaTime;
    }
}
