using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns pre-determined volleys of sea mines according to
/// current stage of tutorial.
/// </summary>
public class TutorialObstacleSpawner : MonoBehaviour
{
    // sea mine spawn support
    [SerializeField] GameObject seaMinePrefab;
    Vector2 panelSize;
    float halfPanelWidth = 0;
    float halfPanelHeight = 0;

    // targeting support
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // retrieve size of nav panel
        panelSize = GetComponent<SpriteRenderer>().size;
        halfPanelWidth = (panelSize.x / 2) - .1f;
        halfPanelHeight = (panelSize.y / 2) - .1f;

        // add self as listener to spawn mine volley event
        EventManager.AddMineVolleyListener(SpawnVolley);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Spawns pre-determined volley of mines based on 
    /// current stage of tutorial.
    /// </summary>
    /// <param name="volleyNumber">pre-determined volley to fire</param>
    void SpawnVolley(int volleyNumber)
    {
        // spawn sea mines corresponding to volley number
        switch (volleyNumber)
        {
            // spawn 1 mine from right side of screen
            case 1:
                SpawnMineAtPanelSide(PanelSides.Right);
                break;
            // spawn mines from top and right of screen
            case 2:
                SpawnMineAtPanelSide(PanelSides.Top);
                SpawnMineAtPanelSide(PanelSides.Right);
                break;
            // spawn mines from bottom and left of screen
            case 3:
                SpawnMineAtPanelSide(PanelSides.Bottom);
                SpawnMineAtPanelSide(PanelSides.Left);
                break;
            default:
                Debug.Log("Error: Invalid volley number.");
                break;
        }
    }

    /// <summary>
    /// Spawns a sea mine at specific side of screen, matching
    /// non-clamped position of player's submarine.
    /// </summary>
    /// <param name="sideToSpawn"></param>
    void SpawnMineAtPanelSide(PanelSides sideToSpawn)
    {
        Debug.Log("spawn: " + sideToSpawn);

        // generate random spawn position
        float randSpawnX = Random.Range(-halfPanelWidth, halfPanelWidth);
        float randSpawnY = Random.Range(-halfPanelHeight, halfPanelHeight);

        // clamp position to one of panel sides
        switch (sideToSpawn)
        {
            case PanelSides.Bottom:
                randSpawnY = -halfPanelHeight;
                break;
            case PanelSides.Left:
                randSpawnX = -halfPanelWidth;
                break;
            case PanelSides.Right:
                randSpawnX = halfPanelWidth;
                break;
            case PanelSides.Top:
                randSpawnY = halfPanelHeight;
                break;
            default:
                break;
        }

        // spawn mine at clamped position and initialize its target
        Vector3 spawnPosition = new Vector3(randSpawnX, randSpawnY);
        GameObject spawnedMine = Instantiate(seaMinePrefab,
            transform.position + spawnPosition, Quaternion.identity);
        spawnedMine.GetComponent<MovingNavPanelIcon>().Target = target;
    }
}
