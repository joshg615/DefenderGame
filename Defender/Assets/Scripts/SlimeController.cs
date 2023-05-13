using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SlimeController : MonoBehaviour
{

    public Transform[] targets; // An array of target transforms
    public float speed = 5.0f; // The speed at which the slime moves towards the target
    public float minDistance = 0.5f; // The minimum distance the slime needs to be from a target to move to the next one
    public float idleTime = 1.0f; // The time the slime waits before moving to the next target

    private int currentTargetIndex = 0; // The index of the current target in the array
    private float idleTimer = 0.0f; // The timer for idle time

    private void Update()
    {
        if (targets != null && targets.Length > 0)
        {
            if (currentTargetIndex == targets.Length)
            {
                Debug.Log("Game won");
            }
            else
            {
                // Get the current target transform
                Transform currentTarget = targets[currentTargetIndex];

                // Calculate the direction towards the current target
                Vector3 direction = currentTarget.position - transform.position;

                // Normalize the direction vector
                direction.Normalize();

                // Move the slime towards the current target
                transform.position += direction * speed * Time.deltaTime;

                // Check if the slime is close enough to the current target
                if (Vector3.Distance(transform.position, currentTarget.position) <= minDistance)
                {
                    // Increase the idle timer
                    idleTimer += Time.deltaTime;

                    // Check if the idle timer has expired
                    if (idleTimer >= idleTime && currentTargetIndex <= targets.Length - 1)
                    {
                        // Move to the next target
                        currentTargetIndex = (currentTargetIndex + 1); //% targets.Length;
                        idleTimer = 0.0f;
                    }
                }
            }

            //if(currentTargetIndex > targets.Length)
            //{
            //    Debug.Log("Game won");
            //}
        }
    }
}
