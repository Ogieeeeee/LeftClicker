namespace LeftClicker;
public class ClientTCP
{
    private readonly TcpClient _client;
    private readonly NetworkStream _stream;
    private readonly List<VKeys> _allowedKeys;
    private static readonly bool[] keyStates = new bool[256]; // Boolean array to track key states

    public ClientTCP(string ipAddress, int port)
    {
        _client = new(ipAddress, port);
        _stream = _client.GetStream();
        _allowedKeys = VirtualKeyHelper.AllowedKeys(VirtualKeyHelper.GetUserClickMode());
    }

    public void Start()
    {
        Console.WriteLine("Starting TCP Client ...");

        _client.NoDelay = true; // Experiment if this is better 
        while (true)
        {
            try
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
                        _stream.Write(data, 0, data.Length);
                    }

                    // Update key state
                    keyStates[keyCode] = keyState;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

