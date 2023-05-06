using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DefenderAbility : PlayerAbility
{
    public float repelForce = 500f; // force applied to repelled objects
    public float repelRadius = 5f; // radius of the repel effect

    // override the HandleInput method to listen for input to activate the ability
    protected override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateAbility();
        }
    }

    // a method to activate the ability
    protected virtual void ActivateAbility()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, repelRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            Rigidbody2D hitRigidbody = hitCollider.GetComponent<Rigidbody2D>();

            if (hitRigidbody != null)
            {
                Vector2 repelDirection = (hitRigidbody.transform.position - transform.position).normalized;
                hitRigidbody.AddForce(repelDirection * repelForce);
            }
        }
    }

    // visualize the repel radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, repelRadius);
    }
}

