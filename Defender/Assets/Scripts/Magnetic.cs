using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will move an object towards a target
public class Magnetic : MonoBehaviour
{
    // Enum for different update modes
    public enum UpdateModes { Update, FixedUpdate, LateUpdate }

    [Header("Magnetic")]
    [Tooltip("The layer mask this magnetic element is attracted to")]
    public LayerMask TargetLayerMask; // The layer mask that determines which objects can be attracted
    [Tooltip("Whether or not to start moving when something on the target layer mask enters this magnetic element's trigger")]
    public bool StartMagnetOnEnter = true; // Determines if the magnetic element starts moving when an object enters its trigger
    [Tooltip("Whether or not to stop moving when something on the target layer mask exits this magnetic element's trigger")]
    public bool StopMagnetOnExit = false; // Determines if the magnetic element stops moving when an object exits its trigger

    [Header("Position Interpolation")]
    [Tooltip("Whether or not to interpolate the movement")]
    public bool InterpolatePosition = true; // Determines if the movement should be interpolated
    [Tooltip("The speed at which to interpolate the follower's movement")]
    public float FollowPositionSpeed = 5f; // The speed of interpolation for the movement
    [Tooltip("The acceleration to apply to the object once it starts following")]
    public float FollowAcceleration = 0.75f; // The acceleration applied to the object when it starts following

    [Header("Mode")]
    [Tooltip("The update at which the movement happens")]
    public UpdateModes UpdateMode = UpdateModes.Update; // The update mode for the movement

    [Header("Debug")]
    [Tooltip("The target to follow, read only, for debug only")]
    public Transform Target; // The target object being followed, for debugging purposes only
    [Tooltip("Whether or not the object is currently following its target's position")]
    public bool FollowPosition = true; // Determines if the object is currently following the target's position

    protected Collider2D _collider2D;
    protected Collider _collider;
    protected Vector3 _velocity = Vector3.zero;
    protected Vector3 _newTargetPosition;
    protected Vector3 _lastTargetPosition;
    protected Vector3 _direction;
    protected Vector3 _newPosition;
    protected float _speed;

    // On Awake we initialize our magnet
    protected virtual void Awake()
    {
        Initialization();
    }

    protected virtual void OnEnable()
    {
        Reset();
    }

    // Grabs the collider and ensures it's set as trigger, initializes the speed
    protected virtual void Initialization()
    {
        _collider2D = this.gameObject.GetComponent<Collider2D>();
        if (_collider2D != null)
        {
            _collider2D.isTrigger = true;
        }

        _collider = this.gameObject.GetComponent<Collider>();
        if (_collider != null)
        {
            _collider.isTrigger = true;
        }

        Reset();
    }

    protected virtual void Reset()
    {
        StopFollowing();
        _speed = 0f;
    }

    // When something enters our trigger, if it's a proper target, we start following it
    protected virtual void OnTriggerEnter2D(Collider2D colliding)
    {
        OnTriggerEnterInternal(colliding.gameObject);
    }

    // When something exits our trigger, if it's a proper target, we stop following it
    protected virtual void OnTriggerExit2D(Collider2D colliding)
    {
        OnTriggerExitInternal(colliding.gameObject);
    }

    // When something enters our trigger, if it's a proper target, we start following it
    protected virtual void OnTriggerEnter(Collider colliding)
    {
        OnTriggerEnterInternal(colliding.gameObject);
    }

    // When something exits our trigger, we stop following it
    protected virtual void OnTriggerExit(Collider colliding)
    {
        OnTriggerExitInternal(colliding.gameObject);
    }

    // Starts following an object we trigger with if conditions are met
    protected virtual void OnTriggerEnterInternal(GameObject colliding)
    {
        if (!StartMagnetOnEnter)
        {
            return;
        }

        if ((TargetLayerMask.value & (1 << colliding.layer)) == 0)
        {
            return;
        }

        Target = colliding.transform;
        StartFollowing();
    }

    // Stops following an object we trigger with if conditions are met
    protected virtual void OnTriggerExitInternal(GameObject colliding)
    {
        if (!StopMagnetOnExit)
        {
            return;
        }

        if ((TargetLayerMask.value & (1 << colliding.layer)) == 0)
        {
            return;
        }

        StopFollowing();
    }

    // At update we follow our target 
    protected virtual void Update()
    {
        if (Target == null)
        {
            return;
        }

        if (UpdateMode == UpdateModes.Update)
        {
            FollowTargetPosition();
        }
    }

    // At fixed update we follow our target 
    protected virtual void FixedUpdate()
    {
        if (UpdateMode == UpdateModes.FixedUpdate)
        {
            FollowTargetPosition();
        }
    }

    // At late update we follow our target 
    protected virtual void LateUpdate()
    {
        if (UpdateMode == UpdateModes.LateUpdate)
        {
            FollowTargetPosition();
        }
    }

    // Follows the target's position based on lerping or not based on what's been defined in the inspector
    protected virtual void FollowTargetPosition()
    {
        // Returns if the target is null to avoid errors
        if (Target == null)
        {
            return;
        }

        // Returns if FollowPosition is not true to avoid following the position
        if (!FollowPosition)
        {
            return;
        }

        // Gets the new target position
        _newTargetPosition = Target.position;

        // Computes the distance between the current position and the target's position.
        float trueDistance = 0f;
        _direction = (_newTargetPosition - this.transform.position).normalized;
        trueDistance = Vector3.Distance(this.transform.position, _newTargetPosition);

        // Changes the speed of following based on the defined acceleration.
        _speed = (_speed < FollowPositionSpeed) ? _speed + FollowAcceleration * Time.deltaTime : FollowPositionSpeed;

        // Lerps distance to interpolate creating a smoother motion vector.
        float interpolatedDistance = trueDistance;
        if (InterpolatePosition)
        {
            interpolatedDistance = Mathf.Lerp(0f, trueDistance, _speed * Time.deltaTime);
            this.transform.Translate(_direction * interpolatedDistance, Space.World);
        }
        else
        {
            this.transform.Translate(_direction * interpolatedDistance, Space.World);
        }
    }

    // Prevents the object from following the target anymore
    public virtual void StopFollowing()
    {
        FollowPosition = false;
    }

    // Makes the object follow the target
    public virtual void StartFollowing()
    {
        FollowPosition = true;
    }

    // Sets a new target for this object to magnet towards
    public virtual void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }
}