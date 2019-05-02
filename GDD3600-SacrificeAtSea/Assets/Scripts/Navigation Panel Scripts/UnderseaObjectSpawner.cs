using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns rocks at random points just outside navigation panel
/// </summary>
public class UnderseaObjectSpawner : MonoBehaviour
{
    #region Fields

    // object spawn support
    [SerializeField] GameObject underseaRockPrefab;
    [SerializeField] GameObject[] cavernWallPrefabs;
    [SerializeField] GameObject underseaTreasurePrefab;
    Vector2 panelSize;
    float halfPanelWidth = 0;
    float halfPanelHeight = 0;

    // seamine spawn delay support
    [SerializeField] float mineMinSpawnDelay = 5f;
    [SerializeField] float minMaxSpawnDelay = 10f;
    float nextSpawnCounter = 0;
    float randSpawnDelay = 0;

    // cavern wall spawn support
    [SerializeField] float cavernSpeed = .25f;
    [SerializeField] Vector3 onScreenSpawnOffset;
    float wallTileWidth = 0.5f;

    // targeting support
    [SerializeField] Transform target;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // retrieve sprite renderer from game object
        panelSize = GetComponent<SpriteRenderer>().size;
        halfPanelWidth = (panelSize.x / 2) - .1f;
        halfPanelHeight = (panelSize.y / 2) - .1f;

        // generate random delay for first mine spawn
        randSpawnDelay = Random.Range(mineMinSpawnDelay, minMaxSpawnDelay);

        // add self as listener to respective events
        EventManager.AddSpawnTreasureListener(SpawnTreasure);
        EventManager.AddScaleObstacleRateListener(ScaleObstacleSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        // if spawn counter exceeds delay time
        if (nextSpawnCounter > randSpawnDelay)
        {
            // reset spawn counter and generate new delay time
            nextSpawnCounter = 0;
            randSpawnDelay = Random.Range(mineMinSpawnDelay, minMaxSpawnDelay);

            // spawn a new undersea rock
            SpawnObjectAtPanelSide(underseaRockPrefab);
        }

        // increment counter by time passed between frames
        nextSpawnCounter += Time.deltaTime;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Spawns new object on random edge of the navigation panel.
    /// Note: Spawned object must have MovingNavPanel component
    /// to access its "Target" property.
    /// </summary>
    /// <param name="objectToSpawn">object to spawn onto nav panel</param>
    void SpawnObjectAtPanelSide(GameObject objectToSpawn)
    {
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

        // spawn object at clamped position and initialize its target
        GameObject objectInstance = Instantiate(objectToSpawn,
            transform.position + spawnPosition, Quaternion.identity);
        objectInstance.GetComponent<MovingNavPanelIcon>().Target = target;
    }

    /// <summary>
    /// Listens for "Spawn Treasure" event and spawns new treasure
    /// object onto the navigation panel.
    /// </summary>
    void SpawnTreasure()
    {
        // spawn treasure onto nav panel
        SpawnObjectAtPanelSide(underseaTreasurePrefab);
    }

    /// <summary>
    /// Listens for "Scale Obstacle Rate" event and scales time between
    /// obstacle spawns.
    /// </summary>
    /// <param name="spawnRateScale"></param>
    void ScaleObstacleSpawnRate(float spawnRateScale)
    {
        // scale max and min spawn delay by given rate
        mineMinSpawnDelay *= spawnRateScale;
        minMaxSpawnDelay *= spawnRateScale;
    }

    #endregion

}
