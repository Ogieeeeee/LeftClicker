namespace LeftClicker;
public class ClientTCP
{
    // Constants for keys to ignore (e.g., keys with virtual key codes 17 and 16)
    readonly TcpClient _client;
    readonly NetworkStream _stream;

    private const int VK_IGNORE1 = 0x11; // VK_CONTROL
    private const int VK_IGNORE2 = 0x10; // VK_SHIFT


    private static bool[] keyStates = new bool[256]; // Boolean array to track key states

    public ClientTCP(string ipAddress, int port)
    {
        _client = new(ipAddress, port);
        _stream = _client.GetStream();
    }

    public void Start()
    {
        Console.WriteLine("Starting TCP Client ...");

        Console.WriteLine(_client.NoDelay);
        _client.NoDelay = true; // Experiment if this is better 
        while (true)
        {
            try
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
                        _stream.Write(data, 0, data.Length);
                    }

                    // Update key state
                    keyStates[keyCode] = keyState;
                }

                // Add a small delay to avoid high CPU usage
                System.Threading.Thread.Sleep(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

