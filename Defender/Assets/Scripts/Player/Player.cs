using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will pilot the player character
public class Player : MonoBehaviour
{
    // associated input manager
    public InputManager linkedInputManager { get; protected set; }

    protected PlayerAbility[] _playerAbilities;
    protected bool _abilitiesCachedOnce = false;
    protected InputManager _inputManager;
    protected PlayerController _controller;

    // Initialize this instance of the player
    protected virtual void Awake()
    {
        Initialize();
    }

    // Gets and stores the input manager and components
    protected virtual void Initialize()
    {
        // we get the current input manager
        SetInputManager();

        // we store our components for further use
        _controller = GetComponent<PlayerController>();

        // Cache abilities
        CacheAbilitiesAtInit();
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
        if (_abilitiesCachedOnce)
        {
            return;
        }
        CacheAbilities();
    }

    // Grabs abilities and cashes them for further use
    public virtual void CacheAbilities()
    {
        // we grab all abilities at our level
        _playerAbilities = this.gameObject.GetComponents<PlayerAbility>();

        _abilitiesCachedOnce = true;
    }

    // Resets the input for all abilities
    public virtual void ResetInput()
    {
        if (_playerAbilities == null)
        {
            return;
        }
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
        // we process our abilities
        EarlyProcessAbilities();
        ProcessAbilities();
        LateProcessAbilities();
    }

    protected virtual void EarlyProcessAbilities()
    {
        foreach (PlayerAbility ability in _playerAbilities)
        {
            if (ability.enabled && ability.abilityInitialized)
            {
                ability.EarlyProcessAbility();
            }
        }
    }

    protected virtual void ProcessAbilities()
    {
        foreach (PlayerAbility ability in _playerAbilities)
        {
            if (ability.enabled && ability.abilityInitialized)
            {
                ability.ProcessAbility();
            }
        }
    }

    protected virtual void LateProcessAbilities()
    {
        foreach (PlayerAbility ability in _playerAbilities)
        {
            if (ability.enabled && ability.abilityInitialized)
            {
                ability.LateProcessAbility();
            }
        }
    }
}
