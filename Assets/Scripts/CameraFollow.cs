using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // The ship to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset relative to the ship
    public float smoothSpeed = 0.125f; // Smoothness of camera movement

    [Header("Threshold Settings")]
    public float positionThreshold = 0.1f; // Minimum movement required to update position
    public float rotationThreshold = 1.0f; // Minimum rotation (degrees) required to update rotation

    [Header("Look At Settings")]
    public bool lookAtTarget = true; // Whether the camera should look at the target

    [SerializeField] private Vector3 velocity = Vector3.zero; // Used for SmoothDamp
    private Vector3 lastTargetPosition;
    private Quaternion lastTargetRotation;

    void Start()
    {
        if (target != null)
        {
            lastTargetPosition = target.position;
            lastTargetRotation = target.rotation;
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for CameraFollow script.");
            return;
        }

        // Calculate the target's current position with offset
        Vector3 desiredPosition = target.position + offset;

        // Calculate the distance the target has moved
        float positionDelta = Vector3.Distance(transform.position, desiredPosition);

        // Update position only if movement exceeds the threshold
        if (positionDelta > positionThreshold)
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }

        // Update rotation only if rotation exceeds the threshold
        if (lookAtTarget)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            float rotationDelta = Quaternion.Angle(transform.rotation, targetRotation);

            if (rotationDelta > rotationThreshold)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
            }
        }
    }
}
