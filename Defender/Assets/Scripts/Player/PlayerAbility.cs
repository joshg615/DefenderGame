using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This class should be overriden to handle a character's ability
public class PlayerAbility : MonoBehaviour
{
    // if true, this ability can perform as usual, if not, it'll be ignored
    public bool abilityPermitted = true;

    public string abilityName;

    // whether or not this ability has been initialized
    public bool abilityInitialized { get { return _abilityInitialized; } }

    protected Player _player;
    protected PlayerController _controller;
    protected PlayerOrientation _orientation;
    protected InputManager _inputManager;
    protected float _verticalInput;
    protected float _horizontalInput;
    protected bool _abilityInitialized = false;
    protected Animator _animator;

    // Determines if this ability is allowed to be used
    public virtual bool AbilityAllowed()
    {
        return abilityPermitted;
    }

    // On awake, we proceed to pre initialize our ability
    protected virtual void Awake()
    {
        PreInitialize();
    }

    // On Start, we call the ability's initialize method
    protected virtual void Start()
    {
        Initialize();
    }

    // A method you can override to have an initialization before the actual initialization
    protected virtual void PreInitialize()
    {
        _player = GetComponent<Player>();
    }

    // Gets and stores components for further use
    protected virtual void Initialize()
    {
        _controller = GetComponent<PlayerController>();
        _orientation = GetComponent<PlayerOrientation>();
        _inputManager = _player.linkedInputManager;
        _abilityInitialized = true;
        _animator = GetComponent<Animator>();
    }

    // Call this any time you want to force this ability to initialize (again)
    public virtual void ForceInitialization()
    {
        Initialize();
    }

    // Internal method to check if an input manager is present or not
    protected virtual void InternalHandleInput()
    {
        if (_inputManager == null) { return; }
        _horizontalInput = _inputManager.primaryMovement.x;
        _verticalInput = _inputManager.primaryMovement.y;
        HandleInput();
    }

    // Called at the very start of the ability's cycle, and intended to be overridden, looks for input and calls methods if conditions are met
    protected virtual void HandleInput()
    {

    }

    // Resets all input for this ability. Can be overridden for ability specific directives
    public virtual void ResetInput()
    {
        _horizontalInput = 0f;
        _verticalInput = 0f;
    }

    // The first of the 3 passes you can have in your ability. Think of it as EarlyUpdate() if it existed
    public virtual void EarlyProcessAbility()
    {
        InternalHandleInput();
    }

    // The second of the 3 passes you can have in your ability. Think of it as Update()
    public virtual void ProcessAbility()
    {

    }

    // The last of the 3 passes you can have in your ability. Think of it as LateUpdate()
    public virtual void LateProcessAbility()
    {

    }

    // Override this to reset this ability's parameters. It'll be automatically called when the character gets killed, in anticipation for its respawn
    public virtual void ResetAbility()
    {

    }
}