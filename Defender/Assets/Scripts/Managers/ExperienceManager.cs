using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles player experience and leveling
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

    // Increases the experience points by the given amount and checks for level up.
    public void AddExperience(int experienceToAdd)
    {
        // Increase the experience points by the given amount
        experiencePoints += experienceToAdd;

        // Check if the player has leveled up
        CheckForLevelUp();
    }

    // Checks whether a player has reached the required amount of experience points to level up
    private void CheckForLevelUp()
    {
        // Check if the experience points have reached or exceeded the required amount for the next level
        if (experiencePoints >= experienceRequiredForNextLevel)
        {
            // Level up the player
            LevelUp();
        }
    }

    // Increases the current level and updates the experience required for the next level.
    public void LevelUp()
    {
        // Calculate the excess experience beyond the required amount
        int excessExperience = experiencePoints - experienceRequiredForNextLevel;

        // Reset the experience points to 0 if negative, then add the excess experience to the next level
        experiencePoints = excessExperience < 0 ? 0 : excessExperience;

        // Calculate the new experience required for the next level, increasing it by 10%
        experienceRequiredForNextLevel = Mathf.RoundToInt(experienceRequiredForNextLevel * 1.1f);

        // Increment the level
        level++;
    }

    // Resets the experience points of the player to 0.
    public void ResetExperience()
    {
        experiencePoints = 0;
    }

    // Adds the specified amount of experience points to the player's total experience.
    public void AddExperiencePoints(int points)
    {
        experiencePoints += points;

        CheckForLevelUp();
    }

    // Returns the current total experience points of the player.
    public int GetCurrentExperience()
    {
        return experiencePoints;
    }

    // Returns the experience points required to reach the next level.
    public int GetRequiredExperienceForNextLevel()
    {
        return experienceRequiredForNextLevel;
    }

    // Sets the required experience points for reaching the specified level.
    public void SetRequiredExperienceForNextLevel(int level, int experiencePoints)
    {
        this.level = level;
        this.experienceRequiredForNextLevel = experiencePoints;
    }

    // Returns the current level of the player.
    public int GetPlayerLevel()
    {
        return level;
    }
}