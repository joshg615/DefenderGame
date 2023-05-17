using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class handles the UI
public class UIManager : MonoBehaviour
{
    // Reference to the fill image of the progress bar
    public Image experienceBar;

    // Reference to the ExperienceManager script
    private ExperienceManager experienceManager;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the experience manager instance
        experienceManager = ExperienceManager.Instance;

        // Set the initial fill amount to zero
        experienceBar.fillAmount = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // Get the normalized experience points from the ExperienceManager
        float normalizedExperience = experienceManager.ExperiencePointsNormalized;

        // If new normalized experience value is higher than current value, use Lerped normalizedExperience value
        if (normalizedExperience > experienceBar.fillAmount)
        {
            experienceBar.fillAmount = Mathf.Lerp(experienceBar.fillAmount, normalizedExperience, Time.deltaTime * 2);
        }
        else // Don't lerp if we're decreasing the value
        {
            experienceBar.fillAmount = normalizedExperience;
        }
    }
}