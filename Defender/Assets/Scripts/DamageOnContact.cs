using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class inflicts damage upon collision with a target that has a Health component
public class DamageOnContact : MonoBehaviour
{
    public LayerMask targetLayerMask; // The LayerMask representing the target layers
    public float damageAmount; // The amount of damage this script causes

    // Responds to collision events and inflicts damage on a collided object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the colliding object's layer is included in the target layer mask
        if (((1 << other.gameObject.layer) & targetLayerMask.value) != 0)
        {
            Health health = other.gameObject.GetComponent<Health>(); // Gets the Health component of the collided object

            // If the collided object has a Health component
            if (health != null)
            {
                health.Damage(damageAmount); // Inflicts damage on the collided object
            }
        }
    }
}
