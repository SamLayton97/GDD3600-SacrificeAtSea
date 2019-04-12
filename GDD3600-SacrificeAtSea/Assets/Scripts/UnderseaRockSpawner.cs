using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns rocks at random points just outside navigation panel
/// </summary>
public class UnderseaRockSpawner : MonoBehaviour
{
    // spawn support
    [SerializeField] GameObject underseaRockPrefab;
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 10f;
    float nextSpawnCounter = 0;
    float randSpawnDelay = 0;

    // targeting support
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // generate random delay for first rock spawn
        randSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        // if spawn counter exceeds delay time
        if (nextSpawnCounter > randSpawnDelay)
        {
            // reset spawn counter and generate new delay time
            nextSpawnCounter = 0;
            randSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

            // TODO: spawn rock at one of nav panel sides
        }

        // increment counter by time passed between frames
        nextSpawnCounter += Time.deltaTime;
    }
}
