using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This persistent singleton handles the inputs and sends commands to the player
public class InputManager : Singleton<InputManager>
{
    // set this to false to prevent the InputManager from reading input
    public bool inputDetectionActive = true;

    // If set to true, acceleration / deceleration will take place when moving / stopping
    public bool smoothMovement = true;
    // the minimum horizontal and vertical value you need to reach to trigger movement on an analog controller
    public Vector2 threshold = new Vector2(0.1f, 0.4f);

    // the primary movement value used to move the player around
    public Vector2 primaryMovement { get { return _primaryMovement; } }
    public Vector2 lastNonNullPrimaryMovement { get; set; }

    protected Vector2 _primaryMovement = Vector2.zero;
    protected string _axisHorizontal;
    protected string _axisVertical;

    // On Awake we run our pre-intialization
    protected override void Awake()
    {
        base.Awake();
        PreInitialize();
    }

    // On Start we look for what mode to use, and initialize our axis and buttons
    protected virtual void Start()
    {
        Initialize();
    }

    // Initializes buttons and axis
    protected virtual void PreInitialize()
    {
        InitializeButtons();
        InitializeAxis();
    }

    // On init we auto detect control schemes
    protected virtual void Initialize()
    {
        ControlsModeDetection();
    }

    // Turn controls on or off depending on whats defined in the inspector
    public virtual void ControlsModeDetection()
    {
        //TODO: 
    }

    // Initializes the buttons. If you want to add more buttons, make sure to register them here
    protected virtual void InitializeButtons()
    {
        //TODO:
    }

    // Initializes the axis strings
    protected virtual void InitializeAxis()
    {
        _axisHorizontal = "Horizontal";
        _axisVertical = "Vertical";
    }

    // At update, we check the various commands and update our values and states accordingly
    protected virtual void Update()
    {
        if (inputDetectionActive)
        {
            SetMovement();
            GetLastNonNullValues();
        }
    }

    // Gets the last non null values for primary axis
    protected virtual void GetLastNonNullValues()
    {
        if (_primaryMovement.magnitude > threshold.x)
        {
            lastNonNullPrimaryMovement = _primaryMovement;
        }
    }

    // Called every frame, gets primary movement values from input
    public virtual void SetMovement()
    {
        if (inputDetectionActive)
        {
            if (smoothMovement)
            {
                _primaryMovement.x = Input.GetAxis(_axisHorizontal);
                _primaryMovement.y = Input.GetAxis(_axisVertical);
            }
            else
            {
                _primaryMovement.x = Input.GetAxisRaw(_axisHorizontal);
                _primaryMovement.y = Input.GetAxisRaw(_axisVertical);
            }
        }
    }
}
