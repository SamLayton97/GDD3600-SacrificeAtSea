using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Script managing player's progress towards level's end
/// </summary>
public class ProgressManager : MonoBehaviour
{
    // level time fields
    [SerializeField] float levelLength = 60;    // time it takes to complete level in seconds

    // progress incrementation support variables
    int currentProgressPercent = 0;
    float timeToIncrementPercentage;
    float percentIncrementCounter = 0;

    // event support
    IncrementProgressEvent incrementProgressEvent;

    // Start is called before the first frame update
    void Start()
    {
        // calculate time to between progress percent increments
        timeToIncrementPercentage = levelLength / 100;

        // adds self as invoker to increment progress event
        incrementProgressEvent = new IncrementProgressEvent();
        EventManager.AddIncrementProgressInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        // if counter exceeds time to increment percentage
        if (percentIncrementCounter >= timeToIncrementPercentage)
        {
            // reset counter
            percentIncrementCounter = 0;

            // increment progress percentage (locked to 100%)
            currentProgressPercent = Mathf.Min(100, currentProgressPercent + 1);
            incrementProgressEvent.Invoke();

            // if player reaches 100% level progress, they win!
            if (currentProgressPercent >= 100)
            {
                // go to level complete scene
                SceneManager.LoadScene("LevelCompleteScene");
            }
        }

        // increment counter
        percentIncrementCounter += Time.deltaTime;
    }

    // Adds given listener to object's increment progress event
    public void AddIncrementProgressListener(UnityAction newListener)
    {
        incrementProgressEvent.AddListener(newListener);
    }
}
