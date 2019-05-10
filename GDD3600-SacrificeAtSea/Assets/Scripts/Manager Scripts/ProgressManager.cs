using System;
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
    #region Fields

    // level time fields
    [SerializeField] float levelLength = 60;    // time it takes to complete level in seconds

    // progress incrementation variables
    int currentProgressPercent = 0;
    float timeToIncrementPercentage;
    float percentIncrementCounter = 0;

    // intermediary evaluation variables
    [SerializeField] int numOfMidlevelEvals = 3;
    int[] evaluationPoints;
    bool isUnscathed = true;
    [SerializeField] DegradingPart[] degradingParts;

    // difficulty scaling variables
    [SerializeField] int routineFluidityThreshold = 50;         // threshold player must reach in 'routine fluidity' to be rewarded with less dense clusters of rocks
    [SerializeField] float passThresholdSpawnScale = 0.9f;      // amount to scale obstacle spawn times should player meet threshold (sparser obstacles)
    [SerializeField] float failThresholdSpawnScale = 0.75f;     // amount to scale obstacle spawn times should player miss threshold (denser obstacles)

    // intermediary eval feedback variables
    [SerializeField] AudioSource passEvalAudioSource;
    [SerializeField] AudioSource failEvalAudioSource;

    // final evaluation support variables
    [SerializeField] GameObject levelCompleteUI;
    [SerializeField] GameObject gameOverUI;
    GameObject endOfLevelUI;
    SubmarineHealthManager subHealthManager;
    TreasureCollectionManager treasureCollectionManager;
    float summedAdaptabilityRatings = 0;
    float highestFunctioningPart= 0;
    int evalsReached = 0;

    // event support
    IncrementProgressEvent incrementProgressEvent;
    SpawnTreasureEvent spawnTreasureEvent;
    ScaleObstacleRateEvent scaleObstacleRateEvent;

    #endregion

    #region Properties

    /// <summary>
    /// Read-access property returning number of 
    /// evaluations manager runs per level.
    /// </summary>
    public int NumberOfMidLevelEvals
    {
        get { return numOfMidlevelEvals; }
    }

    /// <summary>
    /// Returns player's average adaptability rating.
    /// </summary>
    public float AverageAdaptability
    {
        get { return summedAdaptabilityRatings / evalsReached; }
    }

    /// <summary>
    /// Returns health of submarine part with highest functionality.
    /// </summary>
    public float HighestFunctioningPart
    {
        get { return highestFunctioningPart; }
    }

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        // calculate time to between progress percent increments
        timeToIncrementPercentage = levelLength / 100;

        // get references to various manager components
        subHealthManager = GetComponent<SubmarineHealthManager>();
        treasureCollectionManager = GetComponent<TreasureCollectionManager>();

        // adds self as invoker of respective events
        incrementProgressEvent = new IncrementProgressEvent();
        spawnTreasureEvent = new SpawnTreasureEvent();
        scaleObstacleRateEvent = new ScaleObstacleRateEvent();
        EventManager.AddIncrementProgressInvoker(this);
        EventManager.AddSpawnTreasureInvoker(this);
        EventManager.AddScaleObstacleRateInvoker(this);

        // adds self as listener to respective event(s)
        EventManager.AddSubmarineCollisionListener(HandleSubmarineCollision);

        // find evaluation points in level
        evaluationPoints = FindEvaluationPoints(numOfMidlevelEvals);
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

            // if current progress corresponds to one of intermediary evaluation points
            if (Array.Exists(evaluationPoints, element => element == currentProgressPercent))
            {
                // run intermediary evaluation
                RunIntermediaryEvaluation();
            }

            // if player reaches 100% level progress and player has not already completed level, they win!
            if (currentProgressPercent >= 100)
            {
                // create level-complete UI
                CompleteLevel(true);

                // play positive audio feedback
                passEvalAudioSource.Play();
            }
        }

        // increment counter
        percentIncrementCounter += Time.deltaTime;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Finds progress percentages to run intermediary evaluation
    /// </summary>
    /// <param name="evalCount">number of intermediary evaluations to run</param>
    /// <returns>array of percentages corresponding to points in level
    /// to run intermediary evaluation</returns>
    int[] FindEvaluationPoints(int evalCount)
    {
        // create empty list to store eval points
        int[] evalPoints = new int[evalCount];

        // calculate spacing between evaluation points
        int evalSpacing = 100 / (evalCount + 1);

        // for as many eval points as there ought to be in level
        for (int i = 0; i < evalCount; i++)
        {
            // add evaluation point spaced evenly through level
            evalPoints[i] = evalSpacing * (i + 1);
        }

        // return array of eval points
        return evalPoints;
    }

    /// <summary>
    /// Runs intermediary assesment of player's performance, spawning rewards
    /// and adjusting in-game variables as appropriate.
    /// </summary>
    void RunIntermediaryEvaluation()
    {
        // if player made it through portion of level unscathed
        if (isUnscathed)
        {
            // invoke "Spawn Treasure" event
            spawnTreasureEvent.Invoke();
        }

        // assess player's 'routine fluidity' and add to summed total
        float currRoutineFluidity = AssessRoutineFluidity();
        summedAdaptabilityRatings += currRoutineFluidity;

        // if player's routine fluidity grade meets threshold
        if (currRoutineFluidity >= routineFluidityThreshold)
        {
            // scale obstacle spawn rate gradually
            scaleObstacleRateEvent.Invoke(passThresholdSpawnScale);

            // play 'sparser obstacles' sound effect
            passEvalAudioSource.Play();
        }
        // otherwise (failed to meet threshold)
        else
        {
            // scale obstacle spawn rate sharply
            scaleObstacleRateEvent.Invoke(failThresholdSpawnScale);

            // play 'denser obstacles' sound effect
            failEvalAudioSource.Play();
        }

        // reset performance tracking variables
        isUnscathed = true;
    }

    /// <summary>
    /// Returns a number (0-100) representing player's ability to shift tasks
    /// according to new goals. Calculated from (1 - average health / greatest health)
    /// * 100.
    /// </summary>
    /// <returns>adaptability rating of this stage</returns>
    float AssessRoutineFluidity()
    {
        // increment number of evaluations run
        evalsReached++;

        // initialize average and max component health variables
        float totalDegradingPartsHealth = 0;
        float averageDegradingPartHealth = 0;
        float maxDegradingPartHealth = 0;

        // find sum and highest health value of each degrading part
        for (int i = 0; i < degradingParts.Length; i++)
        {
            // add health of current degrading part to total
            float currentHealth = degradingParts[i].CurrentHealth;
            totalDegradingPartsHealth += currentHealth;

            // if part's health is greater than current max, update current max
            if (currentHealth > maxDegradingPartHealth)
            {
                maxDegradingPartHealth = currentHealth;
            }
        }

        // store highest functioning part and calculate average health
        highestFunctioningPart = maxDegradingPartHealth;
        averageDegradingPartHealth = totalDegradingPartsHealth / (float)degradingParts.Length;

        // if average health is not 0, calculate and return measurement of routine fluidity
        if (maxDegradingPartHealth > 0)
            return (1 - (averageDegradingPartHealth / maxDegradingPartHealth)) * 100;

        // otherwise (all parts have health of 0), return 0
        return 0;
    }

    /// <summary>
    /// Listens for submarine collision event, setting unscathed flag to
    /// false if triggered.
    /// </summary>
    void HandleSubmarineCollision()
    {
        // set 'unscathed' flag to false for this section of level
        isUnscathed = false;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Creates instance of end-of-level UI and sets its metrics
    /// </summary>
    public void CompleteLevel(bool isPlayerAlive)
    {
        // if end-of-level UI doesn't already exist
        if (endOfLevelUI == null)
        {
            // run final adaptability evaluation and add to summed total
            float finalAdaptability = AssessRoutineFluidity();
            summedAdaptabilityRatings += finalAdaptability;

            // if player successfully beat level
            if (isPlayerAlive)
            {
                // create instance of level complete UI
                endOfLevelUI = Instantiate(levelCompleteUI);
            }
            // otherwise (player died)
            else
            {
                // create instance of game over UI
                endOfLevelUI = Instantiate(gameOverUI);
            }

            // set metrics of end-of-level UI
            endOfLevelUI.GetComponentInChildren<EndLevelPanelEvaluator>().SetMetrics(subHealthManager.DamageTaken, treasureCollectionManager.TreasureCollected,
                    numOfMidlevelEvals, AverageAdaptability);
        }
    }

    /// <summary>
    /// Adds given listener to object's increment progress event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddIncrementProgressListener(UnityAction newListener)
    {
        incrementProgressEvent.AddListener(newListener);
    }

    /// <summary>
    /// Adds given listener to object's spawn treasure event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddSpawnTreasureListener(UnityAction newListener)
    {
        spawnTreasureEvent.AddListener(newListener);
    }

    /// <summary>
    /// Adds given listener to object's scale obstacle rate event
    /// </summary>
    /// <param name="newListener"></param>
    public void AddScaleObstaclesRateListener (UnityAction<float> newListener)
    {
        scaleObstacleRateEvent.AddListener(newListener);
    }

    #endregion
}
