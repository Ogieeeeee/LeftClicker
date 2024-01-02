namespace LeftClicker;

class Program
{
    private const string IP_ADDRESS = "";
    private const int SERVER_PORT = 12345;

    static void Main()
    {
        StartServicePrompt();
        RunServerOrClient(GetUserServiceChoice());
    }

    private static void StartServicePrompt()
    {
        Console.WriteLine("Which service would you like to start? \n");

        Console.WriteLine("1: Client TCP");
        Console.WriteLine("2: Client UDP");
        Console.WriteLine("3: Server TCP");
        Console.WriteLine("4: Server UDP");
    }


    private static Service GetUserServiceChoice()
    {
        var userServiceChoice = Service.TCPClient;
        var isValidAnswer = false;

        while (!isValidAnswer)
        {
            if (Enum.TryParse(Console.ReadLine(), out userServiceChoice))
            {
                isValidAnswer = true;
            }
        }

        return userServiceChoice;
    }

    private static void RunServerOrClient(Service userChoice)
    {
        switch (userChoice)
        {
            case Service.TCPClient:
                ClientTCP cTCP = new(IP_ADDRESS, SERVER_PORT);
                cTCP.Start();
                break;
            case Service.UDPClient:
                ClientUDP cUDP = new(IP_ADDRESS, SERVER_PORT);
                cUDP.Start();
                break;
            case Service.TCPServer:
                ServerTCP sTCP = new();
                sTCP.Start();
                break;
            case Service.UDPServer:
                ServerUDP sUDP = new();
                sUDP.Start();
                break;
        }
    }

}