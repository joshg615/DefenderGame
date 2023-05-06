using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// This class handles camera follow for Cinemachine powered cameras
public class CameraController : MonoBehaviour
{
    // True if the camera should follow the player
    public bool followsPlayer { get; set; }
    // Whether or not this camera should follow a player
    public bool followsAPlayer = true;
    // the target character this camera should follow
    public Player targetPlayer;

    // A reference to the CinemachineVirtualCamera component
    protected CinemachineVirtualCamera _virtualCamera;

    // On Awake we grab our components
    protected virtual void Awake()
    {
        // Get a reference to the CinemachineVirtualCamera component attached to this object
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // On start we start following the player
    protected virtual void Start()
    {
        // Start following the player when the script is first started
        StartFollowing();
    }

    // Takes a player as a parameter and sets it as the target
    public virtual void SetTarget(Player player)
    {
        // Set the target player for this camera to follow
        targetPlayer = player;
    }

    // Starts following the player
    public virtual void StartFollowing()
    {
        // Only start following if the followsAPlayer flag is set to true
        if (!followsAPlayer) { return; }

        // Set the followsPlayer flag to true and set the camera's follow target to the target player
        followsPlayer = true;
        _virtualCamera.Follow = targetPlayer.cameraTarget.transform;
        _virtualCamera.enabled = true;
    }

    // Stops following the player
    public virtual void StopFollowing()
    {
        // Only stop following if the followsAPlayer flag is set to true
        if (!followsAPlayer) { return; }

        // Set the followsPlayer flag to false and disable the camera
        followsPlayer = false;
        _virtualCamera.Follow = null;
        _virtualCamera.enabled = false;
    }

    // Coroutine to refresh the camera's position
    protected virtual IEnumerator RefreshPosition()
    {
        // Disable the camera while we update its position
        _virtualCamera.enabled = false;
        yield return null;

        // Start following the player again to update the camera's position
        StartFollowing();
    }
}
