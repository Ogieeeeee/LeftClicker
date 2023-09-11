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

    private static string GetUserServiceChoice()
    {
        var userServiceChoice = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(userServiceChoice))
        {
            userServiceChoice = Console.ReadLine();
        }

        return userServiceChoice;
    }

    private static void RunServerOrClient(string userChoice)
    {
        switch (userChoice)
        {
            case "1":
                ClientTCP cTCP = new(IP_ADDRESS, SERVER_PORT);
                cTCP.Start();
                break;
            case "2":
                ClientUDP cUDP = new(IP_ADDRESS, SERVER_PORT);
                cUDP.Start();
                break;
            case "3":
                ServerTCP sTCP = new();
                sTCP.Start();
                break;
            case "4":
                ServerUDP sUDP = new();
                sUDP.Start();
                break;

        }
    }


}