using System.Net;
using System.Net.Sockets;
using System.Text;
using WindowsInput;

public class ServerTCP
{
    public void Start()
    {
        // Set up a TCP listener on a specific port
        int port = 12345;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        server.Start();

        Console.WriteLine($"Server listening on port {port}");

        // Accept incoming client connections
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine($"Client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

        // Create a stream for reading data from the client
        NetworkStream stream = client.GetStream();

        // Create an instance of the InputSimulator class for simulating mouse clicks
        InputSimulator simulator = new InputSimulator();

        // Listen for incoming data
        byte[] buffer = new byte[1024];
        int bytesRead;
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received from client: {data}");

            // Simulate a left mouse click for each key press
            simulator.Mouse.LeftButtonClick();
        }

        // Clean up
        stream.Close();
        client.Close();
        server.Stop();
    }
}