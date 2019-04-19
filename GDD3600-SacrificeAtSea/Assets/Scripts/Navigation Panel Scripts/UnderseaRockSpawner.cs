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
    Vector2 panelSize;
    float halfPanelWidth = 0;
    float halfPanelHeight = 0;

    // spawn delay support
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 10f;
    float nextSpawnCounter = 0;
    float randSpawnDelay = 0;

    // targeting support
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // retrieve sprite renderer from game object
        panelSize = GetComponent<SpriteRenderer>().size;
        halfPanelWidth = (panelSize.x / 2) - .1f;
        halfPanelHeight = (panelSize.y / 2) - .1f;

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

            // generate random spawn position
            float randSpawnX = Random.Range(-halfPanelWidth, halfPanelWidth);
            float randSpawnY = Random.Range(-halfPanelHeight, halfPanelHeight);

            // clamp spawn position to one of panel sides
            PanelSides randSide = (PanelSides)Random.Range((int)0, (int)4);
            switch (randSide)
            {
                case PanelSides.Left:
                    randSpawnX = -halfPanelWidth;
                    break;
                case PanelSides.Right:
                    randSpawnX = halfPanelWidth;
                    break;
                case PanelSides.Bottom:
                    randSpawnY = -halfPanelHeight;
                    break;
                case PanelSides.Top:
                    randSpawnY = halfPanelHeight;
                    break;
                default:
                    break;
            }
            Vector3 spawnPosition = new Vector3(randSpawnX, randSpawnY);

            // spawn rock at clamped position and initialize its target
            GameObject newRock = Instantiate(underseaRockPrefab, 
                transform.position + spawnPosition, Quaternion.identity);
            newRock.GetComponent<MovingNavPanelIcon>().Target = target;
        }

        // increment counter by time passed between frames
        nextSpawnCounter += Time.deltaTime;
    }
}
