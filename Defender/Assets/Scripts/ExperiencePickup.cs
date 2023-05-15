using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    public int experienceToAdd = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // check if the object that this experience object collided with has a tag "Player"
        if (other.CompareTag("Player"))
        {
            ExperienceManager.Instance.AddExperience(experienceToAdd);
            Destroy(gameObject);
        }
    }
}
