using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{ 
    public Transform target; // The transform of the target object
    public float speed = 1.0f; // The speed at which the slime moves towards the target

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = target.position - transform.position;

            // Normalize the direction vector
            direction.Normalize();

            // Move the slime towards the target
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
