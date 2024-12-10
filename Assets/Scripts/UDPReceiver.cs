using UnityEngine;
using extOSC;

public class UDPReceiver : MonoBehaviour
{
    [Header("OSC Settings")]
    public string address = "/facetracker"; // Base OSC address
    public int port = 7000;                 // Listening port (match Python script)

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
        //_receiver.Bind("/facetracker/look/roll", OnReceiveRoll);

        Debug.Log($"Listening for OSC messages on port {port}");
    }

    private void OnReceiveUpDown(OSCMessage message)
    {
        if (message.ToFloat(out float upDown))
        {
            Debug.Log($"Received Up/Down: {upDown}");
            // Use upDown value for actions like head tilt
        }
    }

    private void OnReceiveLeftRight(OSCMessage message)
    {
        if (message.ToFloat(out float leftRight))
        {
            Debug.Log($"Received Left/Right: {leftRight}");
            // Use leftRight value for actions like head turn
        }
    }

    /*private void OnReceiveRoll(OSCMessage message)
    {
        if (message.ToFloat(out float roll))
        {
            Debug.Log($"Received Roll: {roll}");
            // Use roll value for actions like head roll/tilt
        }
    }*/
}
