using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class displays buttons on the screen to test public functions
public class DebuggingButtons : MonoBehaviour
{
    // The player controller we want to add force to
    public PlayerController playerController;

    // The vector3 force to apply when the Add Force button is pressed
    public Vector3 forceVector = new Vector3(1f, 0f, 0f);

    private void OnGUI()
    {
        // Create a button with the label "Add Force" at position (10, 10) on the screen
        if (GUI.Button(new Rect(10, 10, 100, 50), "Add Force"))
        {
            // Call the AddForce method in the player controller with the specified force vector
            playerController.AddForce(forceVector);
        }
    }
}
