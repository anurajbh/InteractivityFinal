using UnityEngine;
using extOSC;

public class UDPReceiver : MonoBehaviour
{
    [Header("OSC Settings")]
    public string address = "/facetracker"; // Base OSC address
    public int port = 7000;                 // Listening port (matches Python script)

    public ShipController shipController;

    private OSCReceiver _receiver;

    void Start()
    {
        // Create an OSC receiver
        _receiver = gameObject.AddComponent<OSCReceiver>();

        // Set the port for receiving OSC messages
        _receiver.LocalPort = port;

        // Bind specific addresses to their handlers
        _receiver.Bind("/facetracker/look/up_down", OnReceiveUpDown);
        _receiver.Bind("/facetracker/look/left_right", OnReceiveLeftRight);
        _receiver.Bind("/facetracker/look/roll", OnReceiveRoll); // Bind roll

        //Debug.Log($"Listening for OSC messages on port {port}");
    }

    private void OnReceiveUpDown(OSCMessage message)
    {
        float upDown = message.Values[0].FloatValue;
        if (upDown > 0)
        {
            shipController.SetThrust(upDown);
        }
        //Debug.Log($"Received Up/Down: {upDown}");
    }

    private void OnReceiveLeftRight(OSCMessage message)
    {
        float leftRight = message.Values[0].FloatValue;
        shipController.SetRotation(leftRight);
        //Debug.Log($"Received Left/Right: {leftRight}");
    }

    private void OnReceiveRoll(OSCMessage message)
    {
        float roll = message.Values[0].FloatValue;
        shipController.SetRoll(roll); // Adjust roll
        //Debug.Log($"Received Roll: {roll}");
    }
}
