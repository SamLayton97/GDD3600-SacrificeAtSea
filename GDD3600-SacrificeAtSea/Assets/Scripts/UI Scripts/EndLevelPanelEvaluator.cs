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

    // rating threshold variables
    [SerializeField] float adaptabilityStarThreshold = 0;
    [SerializeField] float treasureCollectedStarThreshold = 0;

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

    #region Evaluation Methods

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
        hintText.text = HintPrefix + SetRatingAndHint(damage, collectedTreasureRatio, adaptabilityRating);

        // set metrics text to reflect player's final performance
        damageTakenText.text = DamageTakenPrefix + damage;
        treasureCollectedText.text = TreasureCollectedPrefix + collectedTreasure + " / " + treasureInLevel;
        adaptabilityRatingText.text = AdaptabilityRatingPrefix + (int)adaptabilityRating;
    }

    /// <summary>
    /// Sets overall judgement of player's performance (measured from 0 to 3 stars) and provide
    /// a player-specific tip to improve performance.
    /// </summary>
    /// <param name="damageTaken">damage sustained over course of level</param>
    /// <param name="treasureCollectedRatio">amount of collected vs uncollected treasure</param>
    /// <param name="adaptabilityRating">metric representing player's ability to adapt to new routine</param>
    string SetRatingAndHint(int damageTaken, float treasureCollectedRatio, float adaptabilityRating)
    {
        // initialize number of starts to award player
        int starsEarned = 0;

        // if player received less that 100 damage (i.e., they actually beat level)
        if (damageTaken < 100)
        {
            // award 1 star for just beating level
            starsEarned++;

            // if player's treasure collected exceeded threshold, award another
            if (treasureCollectedRatio >= treasureCollectedStarThreshold)
                starsEarned++;

            // if player's adaptability rating exceeded threshold, award yet another star
            if (adaptabilityRating >= adaptabilityStarThreshold)
                starsEarned++;

        }

        // for each star earned, set panel to reflect that
        for (int i = 0; i < starsEarned; i++)
        {
            // set sprite of image to bright variant
            starImages[i].sprite = brightStarSprite;
        }

        // using a decision tree, generate player-specific message
        // if player has odd number of stars
        if (starsEarned % 2 != 0)
        {
            // if player has 3 stars
            if (starsEarned == 3)
            {
                // set hint for top performers
                return "None. You are a master of scavenging for undersea treasure!";
            }
            // otherwise (player has 1 star)
            else
            {
                // if player has taken significant damage
                if (damageTaken >= 60)
                {
                    // flip a coin
                    float coinFlip = Random.Range(0f, 1f);
                    
                    // if coin lands heads
                    if (coinFlip < 0.5f)
                    {
                        // set hint for dodging obstacles
                        return "Time is not on your side. Repair only the essentials before returning to the controls!";
                    }
                    // otherwise (coin landed tails)
                    else
                    {
                        // set hint for recognizing a change in goals
                        return "Remember to return to the submarine controls. Your navigational goals may change during your repair routine!";
                    }
                }
                // otherwise (player didn't take significant damage)
                else
                {
                    // if player has collected any treasure
                    if (treasureCollectedRatio > 0)
                    {
                        // set hint for changing routines to match goals
                        return "When your navigational goals change, so too must your repair routine!";
                    }
                    // otherwise (player hasn't collected any treasure)
                    else
                    {
                        // set hint for earning and collecting treasure
                        return "Coming out unscathed despite strict time restrictions rewards you with treasure.";
                    }
                }
            }
        }
        // otherwise (player has even number of stars)
        else
        {
            // if player has 2 stars
            if (starsEarned == 2)
            {
                // if player has met adaptability threshold
                if (adaptabilityRating >= adaptabilityStarThreshold)
                {
                    // set hint to instruct how to come out unscathed
                    return "Complex maneuvers often require repairing multiple parts.";
                }
                // otherwise (player has only met treasure collection threshold)
                else
                {
                    // set hint to encourage greater routine adaptability
                    return "Make it part of your routine to adapt your routine!";
                }

            }
            // otherwise (player has 0 stars/failed level)
            else
            {
                // if player has very low adaptability rating (<50% of threshold)
                if (adaptabilityRating >= adaptabilityStarThreshold / 2)
                {
                    // set hint to discourage trying to repair everything
                    return "You don't have time to repair everything! Fix only what you need to move around incoming obstacles.";
                }
                // otherwise (player has learned to adapt repair routine)
                else
                {
                    // flip a coin
                    float coinFlip = Random.Range(0f, 1f);

                    // if coin lands heads
                    if (coinFlip < 0.5f)
                    {
                        // set hint to remind player to return to controls after each repair
                        return "Make it a routine to move around obstacles and then make repairs.";
                    }
                    // otherwise (coin lands tails)
                    else
                    {
                        // set hint to remind players to repair malfunctioning parts
                        return "A submarine cannot run on malfunctioning parts! Repair parts of the submarine when the controls no longer work.";
                    }
                }
            }
        }

        // return error string if method broke from decision tree without generating a hint
        return "null";
    }

    #endregion
}
