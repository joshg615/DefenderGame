using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingButtons : MonoBehaviour
{
    // The player controller we want to add force to
    public PlayerController playerController;

    // The vector3 force to apply when the Add Force button is pressed
    public Vector3 forceVector = new Vector3(1f, 0f, 0f);

    // The vector3 movement to set when the Set Movement button is pressed
    public Vector3 movementVector = new Vector3(1f, 0f, 0f);

    // The vector3 position to move to when the Move Position button is pressed
    public Vector3 positionVector = new Vector3(1f, 0f, 0f);

    private void OnGUI()
    {
        // Create a button with the label "Add Force" at position (10, 10) on the screen
        if (GUI.Button(new Rect(10, 10, 100, 50), "Add Force"))
        {
            // Call the AddForce method in the player controller with the specified force vector
            playerController.AddForce(forceVector);
        }

        // Create a button with the label "Set Movement" at position (10, 70) on the screen
        if (GUI.Button(new Rect(10, 70, 100, 50), "Set Movement"))
        {
            // Call the SetMovement method in the player controller with the specified movement vector
            playerController.SetMovement(movementVector);
        }

        // Create a button with the label "Move Position" at position (10, 130) on the screen
        if (GUI.Button(new Rect(10, 130, 100, 50), "Move Position"))
        {
            // Call the MovePosition method in the player controller with the specified position vector
            playerController.MovePosition(positionVector);
        }
    }
}
