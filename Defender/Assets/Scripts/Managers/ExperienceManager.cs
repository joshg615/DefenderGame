using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    // Singleton instance
    public static ExperienceManager Instance { get; private set; }

    public int level;
    public int experiencePoints;
    public int experienceRequiredForNextLevel;
    public float ExperiencePointsNormalized { get { return (float)experiencePoints / experienceRequiredForNextLevel; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddExperience(int experienceToAdd)
    {
        experiencePoints += experienceToAdd;
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        if (experiencePoints >= experienceRequiredForNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        // calculate the excess experience
        int excessExperience = experiencePoints - experienceRequiredForNextLevel;

        // apply the excess experience to the next level
        experiencePoints = 0;
        experiencePoints += excessExperience;

        // calculate new experience required for next level
        experienceRequiredForNextLevel = Mathf.RoundToInt(experienceRequiredForNextLevel * 1.1f);

        // increment the level int
        level++;
    }
}
