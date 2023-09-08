namespace LeftClicker;
public class ClientUDP
{
    private readonly UdpClient _udpClient;
    private readonly List<VKeys> _allowedKeys;
    private static readonly bool[] keyStates = new bool[256]; // Boolean array to track key states

    public ClientUDP(string ipAddress, int port)
    {
        _udpClient = new(ipAddress, port);
        _allowedKeys = VirtualKeyHelper.AllowedKeys(VirtualKeyHelper.GetUserClickMode());
    }

    public void Start()
    {
        try
        {
            while (true)
            {
                // Check the state of each key and mouse button
                for (int keyCode = 1; keyCode <= 255; keyCode++)
                {
                    bool keyState = (Win32Helper.GetAsyncKeyState(keyCode) & 0x8000) != 0;

                    if (!keyStates[keyCode] && keyState && _allowedKeys.Contains((VKeys)keyCode))
                    {
                        // Print the key press event (excluding ignored keys)
                        Console.WriteLine($"Key pressed: {keyCode}");
                        string message = $"{keyCode}";
                        byte[] data = Encoding.ASCII.GetBytes(message);
                        _udpClient.Send(data, data.Length);
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
            _udpClient.Close();
        }
    }
}