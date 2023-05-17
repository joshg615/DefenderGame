using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image progressBarFill; // Reference to the fill image of the progress bar

    private ExperienceManager experienceManager; // Reference to the ExperienceManager script

    private void Start()
    {
        experienceManager = ExperienceManager.Instance;
    }

    private void Update()
    {
        // Get the normalized experience points from the ExperienceManager
        float normalizedExperience = experienceManager.ExperiencePointsNormalized;

        // Set the fill amount of the progress bar's fill image
        progressBarFill.fillAmount = normalizedExperience;
    }
}
