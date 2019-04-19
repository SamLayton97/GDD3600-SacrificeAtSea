using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script managing overhead treasure collection.
/// </summary>
public class TreasureCollectionManager : MonoBehaviour
{
    [SerializeField] int goldValue = 100;

    int currGold = 0;

    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to "Collect Treasure" event
        EventManager.AddCollectTreasureListener(HandleTreasureCollection);
    }

    /// <summary>
    /// Listens for "Collect Treasure" event and adds gold as appropriate
    /// </summary>
    void HandleTreasureCollection()
    {
        // add gold value
        currGold += goldValue;
    }
}
