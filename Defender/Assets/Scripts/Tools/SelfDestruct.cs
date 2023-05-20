using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class destroys the game object after time
public class SelfDestruct : MonoBehaviour
{
    public float destructionDelay = 5f; // Time delay in seconds before self-destruction

    private float startTime; // Variable to store the starting time

    private void Awake()
    {
        startTime = Time.time; // Store the current time when the script is awakened
    }

    private void Update()
    {
        // Check if the time difference between the current time and the start time is greater than or equal to the destruction delay
        if (Time.time - startTime >= destructionDelay)
        {
            Destroy(gameObject); // Destroy the game object
        }
    }
}
