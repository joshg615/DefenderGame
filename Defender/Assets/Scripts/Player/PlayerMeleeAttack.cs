using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : PlayerAbility
{
    public GameObject attackPrefab;  // Prefab to instantiate for the melee attack
    public float attackFrequency = 1f;  // Frequency of the melee attack in seconds
    public Vector3 attackOffset;  // Offset for the instantiated attack prefab

    private float _attackTimer;  // Timer to track the time between attacks

    public override void ProcessAbility()
    {
        // Update the attack timer
        _attackTimer += Time.deltaTime;

        // Check if enough time has passed to perform another attack
        if (_attackTimer >= attackFrequency)
        {
            PerformMeleeAttack();
            _attackTimer = 0f;
        }
    }

    private void PerformMeleeAttack()
    {
        if (!abilityPermitted)
        {
            return;
        }

        // Instantiate the attack prefab with the offset
        if (attackPrefab != null)
        {
            Vector3 offset = attackOffset;
            offset.x *= _orientation.GetFacingDirection(); // Invert x-axis based on sprite orientation

            Instantiate(attackPrefab, transform.position + offset, transform.rotation);
        }
    }

    public override void ResetAbility()
    {
        // Reset the attack timer when the ability is reset
        _attackTimer = 0f;
    }
}
