using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This classmanages the movement of the character
public class Enemy : MonoBehaviour
{
    public float speed = 3.0f; // Movement speed of the enemy
    private static GameObject player; // Reference to the player object
    private Collider2D myCollider; // Collider component of the enemy

    void Awake()
    {
        myCollider = GetComponent<Collider2D>(); // Get the Collider component on Awake
    }

    public void Initialize()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // Find and store the player object
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true); // Activate the enemy game object
    }

    public void Deactivate()
    {
        gameObject.SetActive(false); // Deactivate the enemy game object
    }

    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized; // Calculate direction towards the player
        transform.position += direction * speed * Time.deltaTime; // Move the enemy towards the player based on speed and time

        // Preventing Enemies from Overlapping
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // Check for colliders within a radius of 0.5 units
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != myCollider)
            {
                Vector3 escapeDir = (transform.position - hitCollider.transform.position).normalized; // Calculate escape direction away from the collider
                transform.position += escapeDir * Time.deltaTime; // Move the enemy away from the collider to prevent overlapping
            }
        }
    }
}
