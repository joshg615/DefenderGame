using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the health of a character
public class Health : MonoBehaviour
{
    // The current health of the character
    public float currentHealth;

    // The initial health of the character
    public float initialHealth;

    // The maximum health of the character
    public float maximumHealth;

    // Whether the character is invulnerable
    public bool isInvulnerable;

    // Whether to destroy the character when health reaches zero
    public bool destroyOnDeath;

    // Returns true if this health component can be damaged this frame, and false otherwise
    public virtual bool CanTakeDamageThisFrame()
    {
        // if the object is invulnerable, we do nothing and exit
        if (isInvulnerable)
        {
            return false;
        }

        // if we're already below zero, we do nothing and exit
        if ((currentHealth <= 0f) && (initialHealth != 0f))
        {
            return false;
        }

        return true;
    }

    // Called when the object takes damage
    public virtual void Damage(float damage)
    {
        // Check if we can take damage
        if (!CanTakeDamageThisFrame())
        {
            return;
        }

        // Decrease the character's health by the damage
        SetHealth(currentHealth - damage);

        // If health has fallen below zero, trigger a death
        if (currentHealth <= 0f)
        {
            Kill();
        }
    }

    // Kills the object
    public virtual void Kill()
    {
        if (destroyOnDeath)
        {
            gameObject.SetActive(false);
        }
    }

    // Sets the current health to the specified new value
    public virtual void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
    }
}
