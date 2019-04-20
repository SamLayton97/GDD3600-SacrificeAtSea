using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Evaluates player's performance over course of level, measuring
/// damage taken, treasure collected, routine adaptability, and 
/// an overall score (0-3 stars/wheels).
/// </summary>
public class EndLevelPanelEvaluator : MonoBehaviour
{
    // individual metrics/suggestions variables
    int damageTaken = 0;
    int treasureCollected = 0;
    int possibleTreasure = 0;
    int adaptabilityScore = 0;
    string hint = "";

    // rating display reference variables
    [SerializeField] Image[] starImages;
    [SerializeField] Sprite darkenedStarSprite;
    [SerializeField] Sprite brightStarSprite;

    // scoring display reference variables
    [SerializeField] Text damageTakenText;
    [SerializeField] Text treasureCollectedText;
    [SerializeField] Text adaptabilityRatingText;
    [SerializeField] Text hintText;

    // scoring display helper variables
    const string DamageTakenPrefix = "Damage Taken: ";
    const string TreasureCollectedPrefix = "Treasure Collected: ";
    const string AdaptabilityRatingPrefix = "Adaptability: ";
    const string HintPrefix = "Tip: ";

    /// <summary>
    /// Sets metrics to display on the end-of-level panel
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="collectedTreasure"></param>
    /// <param name="treasureInLevel"></param>
    /// <param name="preciseAdaptabilityRating"></param>
    public void SetMetrics(int damage, int collectedTreasure, int treasureInLevel, float preciseAdaptabilityRating)
    {

    }

}
