using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// This class displays buttons on the screen to test public functions
public class DebugGUI : MonoBehaviour
{
    // Public fields to specify the components to test
    public PlayerController playerController;
    public Health health;

    // Flags to control which buttons are displayed
    public bool showAddForce = true;
    public bool showDamage = true;

    // The vector3 force to apply when the Add Force button is pressed
    public Vector3 forceVector = new Vector3(1f, 0f, 0f);
    // The amount of damage to apply when the Damage button is pressed
    public float damageAmount = 10f;

    // Dictionary to store the button labels and associated actions
    private Dictionary<string, System.Action> buttonActions = new Dictionary<string, System.Action>();

    // Private variables to track the state of the player and buttons
    private bool prevShowAddForce;
    private bool prevShowDamage;

    // Method called once after component's initialization
    private void Awake()
    {
        BuildButtonActions();
    }

    // Method called once per frame
    private void Update()
    {
        //Condition to identify changes in the flags
        if (showAddForce != prevShowAddForce || showDamage != prevShowDamage)
        {
            BuildButtonActions();
        }
    }

    // Method to build buttonActions dictionary of button labels and actions
    private void BuildButtonActions()
    {
        // Clear the dictionary
        buttonActions.Clear();

        // Add the button labels and associated actions to the dictionary
        if (showAddForce)
        {
            buttonActions.Add("Add Force", AddForce);
        }

        if (showDamage)
        {
            buttonActions.Add("Deal Damage", DealDamage);
        }

        // Update the previous values
        prevShowAddForce = showAddForce;
        prevShowDamage = showDamage;
    }

    // Method for rendering button actions on the screen
    private void OnGUI()
    {
        // Create a list of active button labels
        List<string> buttonLabels = buttonActions.Keys.ToList();

        // Dynamically create GUI buttons based on the active button labels
        for (int i = 0; i < buttonLabels.Count; i++)
        {
            if (GUI.Button(new Rect(Screen.width - 160, Screen.height - 60 * (buttonLabels.Count - i), 150, 50), buttonLabels[i]))
            {
                buttonActions[buttonLabels[i]]();
            }
        }
    }

    // Method to apply AddForce action using PlayerController component
    private void AddForce()
    {
        playerController.AddForce(forceVector);
    }

    // Method to apply DealDamage action using Health component
    private void DealDamage()
    {
        health.Damage(damageAmount);
    }
}
