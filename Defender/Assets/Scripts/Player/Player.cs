using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will pilot the player character
public class Player : MonoBehaviour
{
    // associated input manager
    public InputManager linkedInputManager { get; protected set; }
    // an object to use as the camera's point of focus and follow target
    public GameObject cameraTarget { get; set; }

    // An array of the player's abilities
    protected PlayerAbility[] _playerAbilities;
    // Whether or not abilities have been cached
    protected bool _abilitiesCachedOnce = false;
    // The input manager for this player
    protected InputManager _inputManager;
    // The player controller for this player
    protected PlayerController _controller;

    // Initialize this instance of the player
    protected virtual void Awake()
    {
        Initialize();
    }

    // Gets and stores the input manager and components
    protected virtual void Initialize()
    {
        // Get the current InputManager instance
        SetInputManager();

        // Get the PlayerController component attached to this object
        _controller = GetComponent<PlayerController>();

        // Cache the player's abilities
        CacheAbilitiesAtInit();

        // Instantiate the camera target if it's not set already
        if (cameraTarget == null)
        {
            cameraTarget = new GameObject();
        }
        cameraTarget.transform.SetParent(this.transform);
        cameraTarget.transform.localPosition = Vector3.zero;
        cameraTarget.name = "CameraTarget";
    }

    // Gets the InputManager
    public virtual void SetInputManager()
    {
        linkedInputManager = InputManager.Instance;
        _inputManager = InputManager.Instance;
    }

    // Caches abilities if necessary
    protected virtual void CacheAbilitiesAtInit()
    {
        // If abilities have already been cached, return early
        if (_abilitiesCachedOnce)
        {
            return;
        }

        // Otherwise, cache the player's abilities
        CacheAbilities();
    }

    // Grabs abilities and caches them for further use
    public virtual void CacheAbilities()
    {
        // Get all PlayerAbility components attached to this object
        _playerAbilities = this.gameObject.GetComponents<PlayerAbility>();

        // Set the _abilitiesCachedOnce flag to true
        _abilitiesCachedOnce = true;
    }

    // Resets the input for all abilities
    public virtual void ResetInput()
    {
        // If _playerAbilities is null, return early
        if (_playerAbilities == null)
        {
            return;
        }

        // Otherwise, reset the input for each PlayerAbility
        foreach (PlayerAbility ability in _playerAbilities)
        {
            ability.ResetInput();
        }
    }

    // This is called every frame
    protected virtual void Update()
    {
        EveryFrame();
    }

    // We do this every frame. This is separate from Update for more flexibility
    protected virtual void EveryFrame()
    {
        // Process the player's abilities in the appropriate order
        EarlyProcessAbilities();
        ProcessAbilities();
        LateProcessAbilities();
    }

    // Process abilities that need to be processed early
    protected virtual void EarlyProcessAbilities()
    {
        foreach (PlayerAbility ability in _playerAbilities)
        {
            // If the ability is enabled and initialized, process it
            if (ability.enabled && ability.abilityInitialized)
            {
                ability.EarlyProcessAbility();
            }
        }
    }

    // Process abilities that need to be processed normally
    protected virtual void ProcessAbilities()
    {
        foreach (PlayerAbility ability in _playerAbilities)
        {
            // If the ability is enabled and initialized, process it
            if (ability.enabled && ability.abilityInitialized)
            {
                ability.ProcessAbility();
            }
        }
    }

    // Process abilities that need to be processed late
    protected virtual void LateProcessAbilities()
    {
        foreach (PlayerAbility ability in _playerAbilities)
        {
            // If the ability is enabled and initialized, process it
            if (ability.enabled && ability.abilityInitialized)
            {
                ability.LateProcessAbility();
            }
        }
    }
}