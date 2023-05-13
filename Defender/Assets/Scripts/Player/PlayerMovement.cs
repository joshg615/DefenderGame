using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles the player's movement
public class PlayerMovement : PlayerAbility
{
    // The current reference movement speed
    public float movementSpeed { get; set; }

    // If this is true, movement will be forbidden
    public bool movementForbidden { get; set; }

    // Whether or not movement input is authorized at that time
    public bool inputAuthorized = true;

    // Whether or not input should be analog
    public bool analogInput = false;

    // The speed of the character when walking
    public float walkSpeed = 5f;

    // The speed threshold after which the character is not considered idle anymore
    public float idleThreshold = 0.05f;

    // The acceleration to apply to the current speed
    public float acceleration = 0f;

    // The deceleration to apply to the current speed
    public float deceleration = 0f;

    // Whether or not to interpolate movement speed
    public bool interpolateMovementSpeed = false;
    public float movementSpeedMaxMultiplier { get; set; } = float.MaxValue;
    private float _movementSpeedMultiplier;

    // The multiplier to apply to the horizontal movement
    public float movementSpeedMultiplier
    {
        get => Mathf.Min(_movementSpeedMultiplier, movementSpeedMaxMultiplier);
        set => _movementSpeedMultiplier = value;
    }

    protected float _movementSpeed;
    protected float _horizontalMovement;
    protected float _verticalMovement;
    protected Vector3 _movementVector;
    protected Vector2 _currentInput = Vector2.zero;
    protected Vector2 _normalizedInput;
    protected Vector2 _lerpedInput = Vector2.zero;
    protected float _acceleration = 0f;

    // On Initialize, we set our movement speed to walkSpeed
    protected override void Initialize()
    {
        base.Initialize();
        ResetAbility();
    }

    // Resets character movement speeds
    public override void ResetAbility()
    {
        base.ResetAbility();
        movementSpeed = walkSpeed;
        movementSpeedMultiplier = 1f;
        movementForbidden = false;
    }

    // The second of the 3 passes you can have in your ability. Think of it as Update()
    public override void ProcessAbility()
    {
        base.ProcessAbility();

        HandleMovement();
    }

    // Called at Update(), handles horizontal movement
    public virtual void HandleMovement()
    {
        // If movement is prevented, we exit and do nothing
        if (!abilityPermitted)
        {
            return;
        }

        if (movementForbidden)
        {
            _horizontalMovement = 0f;
            _verticalMovement = 0f;
        }

        SetMovement();
    }

    // Called at the very start of the ability's cycle. Looks for input and calls methods if conditions are met
    protected override void HandleInput()
    {
        if (inputAuthorized)
        {
            _horizontalMovement = _horizontalInput;
            _verticalMovement = _verticalInput;
        }
        else
        {
            _horizontalMovement = 0f;
            _verticalMovement = 0f;
        }
    }

    // Moves the controller
    protected virtual void SetMovement()
    {
        // Reset vectors to zero
        _movementVector = Vector3.zero;
        _currentInput = Vector2.zero;

        // Get the current input from the movement variables
        _currentInput.x = _horizontalMovement;
        _currentInput.y = _verticalMovement;

        // Normalize input
        _normalizedInput = _currentInput.normalized;

        // Initialize variable for movement interpolation
        float interpolationSpeed = 1f;

        // Handle analog and digital input differently
        if ((acceleration == 0) || (deceleration == 0))
        {
            _lerpedInput = analogInput ? _currentInput : _normalizedInput;
        }
        else
        {
            
            // If input is zero, decrease speed
            if (_normalizedInput.magnitude == 0)
            {
                _acceleration = Mathf.Lerp(_acceleration, 0f, deceleration * Time.deltaTime);
                _lerpedInput = Vector2.Lerp(_lerpedInput, _lerpedInput * _acceleration, Time.deltaTime * deceleration);
                interpolationSpeed = deceleration;
            }
            // If input is not zero, increase speed
            else
            {
                _acceleration = Mathf.Lerp(_acceleration, 1f, acceleration * Time.deltaTime);
                _lerpedInput = analogInput ? Vector2.ClampMagnitude(_currentInput, _acceleration) : Vector2.ClampMagnitude(_normalizedInput, _acceleration);
                interpolationSpeed = acceleration;
            }
        }

        // Update the movement vector with the interpolated input
        _movementVector.x = _lerpedInput.x;
        _movementVector.y = 0f;
        _movementVector.z = _lerpedInput.y;

        // Interpolate the movement speed if necessary
        if (interpolateMovementSpeed)
        {
            _movementSpeed = Mathf.Lerp(_movementSpeed, movementSpeed * movementSpeedMultiplier, interpolationSpeed * Time.deltaTime);
        }
        else
        {
            _movementSpeed = movementSpeed * movementSpeedMultiplier;
        }

        // Update the movement vector with the movement speed
        _movementVector *= _movementSpeed;

        // Clamp the magnitude of the movement vector if necessary
        if (_movementVector.magnitude > movementSpeed * movementSpeedMultiplier)
        {
            _movementVector = Vector3.ClampMagnitude(_movementVector, movementSpeed);
        }

        // If the input is zero and the current movement is zero, set movement vector to zero
        if ((_currentInput.magnitude <= idleThreshold) && (_controller.currentMovement.magnitude < idleThreshold))
        {
            _movementVector = Vector3.zero;
        }

        // Apply the movement vector to the controller
        _controller.SetMovement(_movementVector);
    }
}
