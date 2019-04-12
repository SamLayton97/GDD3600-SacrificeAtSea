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

    // targeting support
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // DEBUGGING: spawn undersea rock to test force functionality
        GameObject newRock = Instantiate(underseaRockPrefab,
            new Vector2(16, 0.5f), Quaternion.identity);
        newRock.GetComponent<UnderseaRock>().Target = target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
