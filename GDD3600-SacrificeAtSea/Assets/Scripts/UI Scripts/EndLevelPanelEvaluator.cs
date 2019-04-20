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
    /// <param name="damage">damage sustained during level</param>
    /// <param name="collectedTreasure">treasure collected during level</param>
    /// <param name="treasureInLevel">possible amount of treasure to collect in level</param>
    /// <param name="adaptabilityRating">un-rounded adaptability rating</param>
    public void SetMetrics(int damage, int collectedTreasure, int treasureInLevel, float adaptabilityRating)
    {
        // calculate ratio of collected treasure to uncollected treasure
        float collectedTreasureRatio = collectedTreasure / treasureInLevel;

        // from inputs, set star rating (0-3)
        SetStarRating(damage, collectedTreasureRatio, adaptabilityRating);

        // set metrics text to reflect player's final performance
        damageTakenText.text = DamageTakenPrefix + damage;
        treasureCollectedText.text = TreasureCollectedPrefix + collectedTreasure + " / " + treasureInLevel;
        adaptabilityRatingText.text = AdaptabilityRatingPrefix + (int)adaptabilityRating;

        // set hint text to message specific to player's performance
        hintText.text = HintPrefix + GenerateHint(damage, collectedTreasure, adaptabilityRating);

    }

    /// <summary>
    /// Sets overall judgement of player's performance (measured from 0 to 3 stars)
    /// </summary>
    /// <param name="damageTaken">damage sustained over course of level</param>
    /// <param name="treasureCollectedRation">amount of collected vs uncollected treasure</param>
    /// <param name="adaptabilityRating">metric representing player's ability to adapt to new routine</param>
    void SetStarRating(int damageTaken, float treasureCollectedRation, float adaptabilityRating)
    {

    }

    /// <summary>
    /// Generate hint specific to player's performance
    /// </summary>
    /// <param name="damageTaken">damage sustained over course of level</param>
    /// <param name="treasureCollectedRatio">amount of collected vs uncollected treasure</param>
    /// <param name="adaptabilityRating">metric representing player's ability to adapt to new routine</param>
    /// <returns>message specific to player's performance</returns>
    string GenerateHint(int damageTaken, float treasureCollectedRatio, float adaptabilityRating)
    {
        // DUMMY CODE: return test string
        return "TEST TEST TEST";
    }

}
