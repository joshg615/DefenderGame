using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // the current speed of the character
    public Vector3 speed;
    // the current velocity of the character
    public Vector3 velocity;
    // the velocity of the character last frame
    public Vector3 velocityLastFrame;
    // the current acceleration of the character
    public Vector3 acceleration;
    // the current movement of the character
    public Vector3 currentMovement;
    // the direction the character is going in
    public Vector3 currentDirection;
    // the current added force, to be added to the character's movement
    public Vector3 addedForce;

    protected Vector3 _positionLastFrame;
    protected Vector3 _impact;
    protected Vector3 _orientedMovement;

    protected Rigidbody2D _rigidBody;

    // On awake, we initialize our current direction and grab our components
    protected void Awake()
    {
        currentDirection = transform.forward;

        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // On update, we determine the direction and acceleration
    protected void Update()
    {
        DetermineDirection();
        DetermineAcceleration();
    }

    // On late update, computes the speed of the agent
    protected void LateUpdate()
    {
        velocityLastFrame = velocity;
        ComputeSpeed();
    }

    // On fixed update, we move our rigidbody
    protected void FixedUpdate()
    {
        ApplyImpact();

        // Calculate a new movement vector based on the currentMovement vecotr and any additional force that may be applied
        Vector2 newMovement = _rigidBody.position + (Vector2)(currentMovement + addedForce) * Time.fixedDeltaTime;

        // Move to a new position based on the specified movement vector
        _rigidBody.MovePosition(newMovement);
    }

    // Adds a force of the specified vector
    public void AddForce(Vector3 movement)
    {
        Impact(movement.normalized, movement.magnitude);
    }

    // Another way to add a force of the specified force and direction
    public void Impact(Vector3 direction, float force)
    {
        direction = direction.normalized;
        _impact += direction.normalized * force;
    }

    // Sets the current movement
    public void SetMovement(Vector3 movement)
    {
        _orientedMovement = movement;
        _orientedMovement.y = _orientedMovement.z;
        _orientedMovement.z = 0f;
        currentMovement = _orientedMovement;
    }

    // Tries to move to the specified position
    public void MovePosition(Vector3 newPosition)
    {
        // TODO: Fix. Seems to be conflicting with fixed update for now
        _rigidBody.MovePosition(newPosition);
    }

    // Resets all values for this controller
    public void Reset()
    {
        _impact = Vector3.zero;
        speed = Vector3.zero;
        velocity = Vector3.zero;
        velocityLastFrame = Vector3.zero;
        acceleration = Vector3.zero;
        currentMovement = Vector3.zero;
        currentDirection = Vector3.zero;
        addedForce = Vector3.zero;
    }

    // Determines the controller's current direction based on its movement vector
    protected void DetermineDirection()
    {
        if (currentMovement != Vector3.zero)
        {
            currentDirection = currentMovement.normalized;
        }
    }

    // Calculates the current acceleration based on its current velocity and its velocity in the previous frame
    protected void DetermineAcceleration()
    {
        velocity = (_rigidBody.transform.position - _positionLastFrame) / Time.deltaTime;
        acceleration = (velocity - velocityLastFrame) / Time.deltaTime;
    }

    // Computes the speed by comparing its current position to its position in the previous frame
    protected void ComputeSpeed()
    {
        if (Time.deltaTime != 0f)
        {
            speed = (this.transform.position - _positionLastFrame) / Time.deltaTime;
        }
        // we round the speed to 2 decimals
        speed.x = Mathf.Round(speed.x * 100f) / 100f;
        speed.y = Mathf.Round(speed.y * 100f) / 100f;
        speed.z = Mathf.Round(speed.z * 100f) / 100f;
        _positionLastFrame = this.transform.position;
    }

    // Apply an impact force and gradually reduce its strength over time
    protected void ApplyImpact()
    {
        // Check if the impact force is strong enough to be applied
        if (_impact.magnitude > 0.2f)
        {
            // Apply the impact force. The _impact vector represents the direction and strength of the impact force
            _rigidBody.AddForce(_impact);
        }
        // gradually reduce the strength of the _impact force towards zero, so that the impact force becomes weaker and weaker over time
        _impact = Vector3.Lerp(_impact, Vector3.zero, 5f * Time.deltaTime);
    }
}
