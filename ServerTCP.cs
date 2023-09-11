namespace LeftClicker;

public class ServerTCP
{
    public void Start()
    {
        // Set up a TCP listener on a specific port
        int port = 12345;
        TcpListener server = new TcpListener(IPAddress.Any, port);
        TcpClient? client = null;
        NetworkStream? stream = null;
        try
        {

            server.Start();

            // Accept incoming client connections
            client = server.AcceptTcpClient();
            Console.WriteLine($"Client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

            // Create a stream for reading data from the client
            stream = client.GetStream();

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
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Start();
        }
        finally
        {
            // Clean up
            stream?.Close();
            client?.Close();
            server.Stop();
        }
    }
}