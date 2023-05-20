using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrowAbility : PlayerAbility
{
    public GameObject hammerPrefab;
    public Transform hammerSpawnPoint;
    public float throwForce = 10f;
    public float throwDelay = 0.5f;

    private bool isThrowing = false;

    protected override void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartThrowing();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopThrowing();
        }
    }

    private void StartThrowing()
    {
        if (!isThrowing)
        {
            isThrowing = true;
            StartCoroutine(ThrowRepeatedly());
        }
    }

    private void StopThrowing()
    {
        if (isThrowing)
        {
            isThrowing = false;
            StopCoroutine(ThrowRepeatedly());
        }
    }

    private IEnumerator ThrowRepeatedly()
    {
        while (isThrowing)
        {
            ThrowProjectile();

            yield return new WaitForSeconds(throwDelay);
        }
    }

    private void ThrowProjectile()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(hammerPrefab, hammerSpawnPoint.position, Quaternion.identity);

        // Calculate the throw direction based on the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = mousePosition - (Vector2)hammerSpawnPoint.position;
        throwDirection.Normalize();

        // Get the projectile's rigidbody component
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        // Apply the throw force to the projectile
        projectileRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
    }
}
