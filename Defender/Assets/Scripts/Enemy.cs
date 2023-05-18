using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This classmanages the movement of the character
public class Enemy : MonoBehaviour
{
    public enum TargetType { Player, Slime }
    public TargetType target = TargetType.Player;

    public float speed = 3.0f; // Movement speed of the enemy
    private static GameObject player; // Reference to the player object
    private static GameObject slime;
    private Collider2D myCollider; // Collider component of the enemy

    void Awake()
    {
        myCollider = GetComponent<Collider2D>(); // Get the Collider component on Awake
        Initialize();
    }

    public void Initialize()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (slime == null)
        {
            slime = GameObject.FindGameObjectWithTag("Slime"); // Find and store the slime object
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
        // Choose the target based on the enum
        GameObject currentTarget = target == TargetType.Player ? player : slime;

        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != myCollider)
            {
                Vector3 escapeDir = (transform.position - hitCollider.transform.position).normalized;
                transform.position += escapeDir * Time.deltaTime;
            }
        }
    }
}
