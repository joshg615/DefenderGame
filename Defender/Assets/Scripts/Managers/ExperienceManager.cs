using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    // Singleton instance
    public static ExperienceManager Instance { get; private set; }

    // Variables to track level and experience points
    public int level; // Current level
    public int experiencePoints; // Current experience points
    public int experienceRequiredForNextLevel; // Experience required to reach the next level

    // Normalized experience points, ranging from 0 to 1
    public float ExperiencePointsNormalized { get { return (float)experiencePoints / experienceRequiredForNextLevel; } }

    private void Awake()
    {
        // Check if an instance of ExperienceManager already exists
        if (Instance == null)
        {
            // Set the current instance as the singleton instance
            Instance = this;
        }
        else
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
        }
    }

    public void AddExperience(int experienceToAdd)
    {
        // Increase the experience points by the given amount
        experiencePoints += experienceToAdd;

        // Check if the player has leveled up
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        // Check if the experience points have reached or exceeded the required amount for the next level
        if (experiencePoints >= experienceRequiredForNextLevel)
        {
            // Level up the player
            LevelUp();
        }
    }

    public void LevelUp()
    {
        // Calculate the excess experience beyond the required amount
        int excessExperience = experiencePoints - experienceRequiredForNextLevel;

        // Reset the experience points to 0 and add the excess experience to the next level
        experiencePoints = 0;
        experiencePoints += excessExperience;

        // Calculate the new experience required for the next level, increasing it by 10%
        experienceRequiredForNextLevel = Mathf.RoundToInt(experienceRequiredForNextLevel * 1.1f);

        // Increment the level
        level++;
    }
}
