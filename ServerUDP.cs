namespace LeftClicker;
public class ServerUDP
{
    public void Start()
    {
        // Set up a UDP listener on a specific port
        int port = 12345;
        UdpClient udpListener = new UdpClient(port);
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, port);

        Console.WriteLine($"Server listening on port {port}");

        // Create an instance of the InputSimulator class for simulating mouse clicks
        InputSimulator simulator = new InputSimulator();

        try
        {
            while (true)
            {
                // Listen for incoming UDP packets
                byte[] receiveBytes = udpListener.Receive(ref clientEndPoint);
                string data = Encoding.ASCII.GetString(receiveBytes);
                Console.WriteLine($"Received from client: {data}");

                // Simulate a left mouse click for each key press
                simulator.Mouse.LeftButtonClick();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Clean up
            udpListener.Close();
        }
    }
}