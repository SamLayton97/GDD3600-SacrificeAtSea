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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Spawns a sea mine at specific side of screen, matching
    /// non-clamped position of player's submarine.
    /// </summary>
    /// <param name="sideToSpawn"></param>
    void SpawnMineAtPanelSide(PanelSides sideToSpawn)
    {
        // initialize spawn position to match submarine
        Vector3 spawnPosition = target.position;

        // clamp position to one of panel sides
        switch (sideToSpawn)
        {
            case PanelSides.Bottom:
                spawnPosition.y = -halfPanelHeight;
                break;
            case PanelSides.Left:
                spawnPosition.x = -halfPanelWidth;
                break;
            case PanelSides.Right:
                spawnPosition.x = halfPanelWidth;
                break;
            case PanelSides.Top:
                spawnPosition.y = halfPanelHeight;
                break;
            default:
                break;
        }

        // spawn mine at clamped position and initialize its target
        GameObject spawnedMine = Instantiate(seaMinePrefab,
            transform.position + spawnPosition, Quaternion.identity);
        spawnedMine.GetComponent<MovingNavPanelIcon>().Target = target;
    }
}
