using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for changing the orientation of the player's sprite based on movement direction
public class PlayerOrientation : PlayerAbility
{
    // A reference to the player's SpriteRenderer
    private SpriteRenderer _spriteRenderer;

    // Bool to determine if the sprite is originally facing left
    public bool invertXAxis = false;

    protected override void Initialize()
    {
        base.Initialize();
        // Grab the SpriteRenderer component from the player GameObject
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Override the ProcessAbility method to add functionality for changing sprite orientation based on movement direction
    public override void ProcessAbility()
    {
        base.ProcessAbility();

        // Check if the player is moving
        if (_controller.isWalking)
        {
            // If the sprite is originally facing left
            if (invertXAxis)
            {
                // If the player is moving to the right
                if (_controller.currentDirection.x > 0)
                {
                    // Flip the sprite to face right
                    _spriteRenderer.flipX = true;
                }
                // If the player is moving to the left
                else if (_controller.currentDirection.x < 0)
                {
                    // Flip the sprite to face left
                    _spriteRenderer.flipX = false;
                }
            }
            else // If the sprite is originally facing right
            {
                // If the player is moving to the left
                if (_controller.currentDirection.x < 0)
                {
                    // Flip the sprite to face left
                    _spriteRenderer.flipX = true;
                }
                // If the player is moving to the right
                else if (_controller.currentDirection.x > 0)
                {
                    // Flip the sprite to face right
                    _spriteRenderer.flipX = false;
                }
            }
        }
    }
}