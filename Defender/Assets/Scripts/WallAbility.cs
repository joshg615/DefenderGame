using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAbility : PlayerAbility
{
    // Public variable to determine the size of the wall to be created
    public float wallSize = 1f;

    // Public variable to determine the height of the wall to be created
    public float wallHeight = 1f;

    // Public variable to determine the force of the player's downward smash
    public float smashForce = 10f;

    // Public variable to determine the cooldown time of the ability
    public float cooldownTime = 2f;

    // Private variable to store the time when the ability was last used
    private float _lastUsedTime = -Mathf.Infinity;

    // Override the HandleInput method to check for the input for this ability
    protected override void HandleInput()
    {
        // Check if the ability is allowed and if the player presses the ability button
        if (AbilityAllowed() && Input.GetKeyDown(KeyCode.E))
        {
            // Get the direction in which the player is facing
            Vector2 direction = _controller.currentDirection;
            GameObject wall;

            // Apply a downward force to the player
            //_controller.SetVerticalForce(-smashForce);

            // Create a wall in the direction the player is facing
            Vector2 wallPosition =  new Vector2(transform.position.x + direction.x * 2, transform.position.y + direction.y * 2);
            if(direction.x > 0 || direction.x < 0)
            {
                
                wall =Instantiate(Resources.Load("Wall"), wallPosition, Quaternion.Euler(0, 0, 90)) as GameObject;
                wall.transform.localScale = new Vector3(wallSize, wallHeight, 1f);
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall"), wallPosition, Quaternion.identity) as GameObject;
                wall.transform.localScale = new Vector3(wallSize, wallHeight, 1f);
            }
           
            // Set the last used time to the current time
            _lastUsedTime = Time.time;
        }
    }

    public override bool AbilityAllowed()
    {
        if( Time.time <= _lastUsedTime + cooldownTime)
        {
            return false;
        }
        return true; 
    }
}

