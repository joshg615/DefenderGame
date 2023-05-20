using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAbility : PlayerAbility
{
    public float repelForce = 500f; // force applied to repelled objects
    public float repelRadius = 5f; // radius of the repel effect
    public LayerMask repelLayerMask; // layer mask for colliders to repel
    public GameObject shieldObject;
    private CircleCollider2D _shieldCollider; // the collider component of the shield object


    // override the Initialize method to get the renderer and collider components of the shield object
    protected override void Initialize()
    {
        base.Initialize();

        if (shieldObject != null)
        {
            //_shieldRenderer = shieldObject.GetComponent<Renderer>();
            _shieldCollider = shieldObject.GetComponent<CircleCollider2D>();
        }
    }
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
        
        // turn on the shield object
        if (shieldObject != null)
        {
            //_shieldRenderer.enabled = true;
            shieldObject.SetActive(true);
            _shieldCollider.enabled = true;
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _shieldCollider.radius, repelLayerMask);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Rigidbody2D hitRigidbody = hitCollider.GetComponent<Rigidbody2D>();

            if (hitRigidbody != null)
            {
                Vector2 repelDirection = (hitRigidbody.transform.position - transform.position).normalized;
                hitRigidbody.AddForce(repelDirection * repelForce);
            }
        }

        Invoke("ResetAbility", 3f);

    }

    // override the ResetAbility method to turn off the shield object when the ability is reset
    public override void ResetAbility()
    {
        base.ResetAbility();

        // turn off the shield object
        if (shieldObject != null)
        {
            //_shieldRenderer.enabled = false;
            _shieldCollider.enabled = false;
            shieldObject.SetActive(false);
        }
    }

    // visualize the repel radius in the editor
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, repelRadius);
    //}
}

