using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    // Amount of experience to add when picked up
    public int experienceToAdd = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that this experience object collided with has a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Add the specified amount of experience to the player's experience points
            ExperienceManager.Instance.AddExperience(experienceToAdd);

            // Destroy the experience pickup object
            Destroy(gameObject);
        }
    }
}
