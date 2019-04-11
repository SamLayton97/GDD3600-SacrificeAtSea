using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Swaps camera's target based on player's relative position to parts of the submarine
/// </summary>
public class CameraTargetSwapper : MonoBehaviour
{
    // event support
    SwapCameraTargetEvent swapCameraTargetEvent;

    // Start is called before the first frame update
    void Start()
    {
        // add self as invoker of swap camera target event
        swapCameraTargetEvent = new SwapCameraTargetEvent();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds listener to object's swap camera target event
    /// </summary>
    /// <param name="listener"></param>
    public void AddSwapCameraTargetListener(UnityAction<Transform> listener)
    {
        swapCameraTargetEvent.AddListener(listener);
    }
}
