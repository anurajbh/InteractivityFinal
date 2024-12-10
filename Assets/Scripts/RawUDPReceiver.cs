using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RawUDPReceiver : MonoBehaviour
{
    public int port = 5005; // Match this with the facetracker's port
    private UdpClient udpClient;

    void Start()
    {
        udpClient = new UdpClient(port);
        udpClient.BeginReceive(ReceiveCallback, null);
        Debug.Log($"Listening for raw UDP data on port {port}");
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] data = udpClient.EndReceive(ar, ref endPoint);

        // Convert binary data to string for debug purposes (if applicable)
        string receivedText = Encoding.UTF8.GetString(data);
        Debug.Log($"Received Data: {receivedText}");

        // Process raw data here if necessary

        udpClient.BeginReceive(ReceiveCallback, null); // Continue listening
    }

    private void OnDestroy()
    {
        udpClient.Close();
    }
}
