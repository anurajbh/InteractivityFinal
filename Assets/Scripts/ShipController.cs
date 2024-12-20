using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Thrust Settings")]
    [SerializeField] float thrustPower = 10f;

    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] float rollSpeed = 50f;

    private Rigidbody _rigidbody;

    [SerializeField] float thrustSensitivity = 20f;
    [SerializeField] float rotationSensitivity = 2f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetThrust(float normalizedThrust)
    {
        // Map normalized input (0 to 1) to thrust power
        float thrust = Mathf.Clamp01(normalizedThrust) * thrustPower *thrustSensitivity;

        // Align thrust to the ship's "up" direction (local Y-axis)
        Vector3 force = transform.up * thrust;
        _rigidbody.AddForce(force, ForceMode.Acceleration);

        // Debug.Log($"Applying Thrust: {force}");
    }

    public void SetRotation(float horizontalInput)
    {
        // Map horizontal input (-1 to 1) to rotation speed
        float rotation = Mathf.Clamp(horizontalInput, -1f, 1f) * rotationSpeed * Time.deltaTime *rotationSensitivity;

        // Apply rotation around the ship's local Z-axis (yaw rotation relative to upward default)
        Quaternion deltaRotation = Quaternion.Euler(0f, 0f, -rotation);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

       // Debug.Log($"Applying Rotation: {rotation}");
    }
    public void SetRoll(float roll)
    {
        Vector3 torque = transform.forward * (roll * rollSpeed);
        _rigidbody.AddTorque(torque);
        //Debug.Log($"Applying Roll: {torque}");
    }
}
