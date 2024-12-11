using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The ship to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset relative to the ship
    public float smoothSpeed = 0.125f; // Smoothness of camera movement

    [Header("Look At Settings")]
    public bool lookAtTarget = true; // Whether the camera should look at the target

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for CameraFollow script.");
            return;
        }

        // Calculate the desired position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the target
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
