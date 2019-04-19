using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script managing overhead treasure collection.
/// </summary>
public class TreasureCollectionManager : MonoBehaviour
{
    // gold tracking variables
    [SerializeField] int goldValue = 100;
    int currGold = 0;

    // event support variables
    AddGoldCollectedEvent addGoldCollectedEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of "Update Gold Collected" event
        addGoldCollectedEvent = new AddGoldCollectedEvent();
        EventManager.AddGoldInvoker(this);

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

        // update gold-collected UI
        addGoldCollectedEvent.Invoke(currGold);
    }

    /// <summary>
    /// Adds given method as listener to object's add gold collected event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddGoldCollectedListener(UnityAction<int> newListener)
    {
        addGoldCollectedEvent.AddListener(newListener);
    }
}
