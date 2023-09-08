
class Program
{
    static void Main()
    {
        Console.WriteLine("Which service would you like to start? \n");

        Console.WriteLine("1: Client TCP");
        Console.WriteLine("2: Client UDP");
        Console.WriteLine("3: Server TCP");
        Console.WriteLine("4: Server UDP");

        string? userInput = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(userInput))
        {
            userInput = Console.ReadLine();
        }

        switch (userInput)
        {

            case "1":
                ClientTCP cTCP = new();
                cTCP.Start();
                break;
            case "2":
                ClientUDP cUDP = new();
                break;
            case "3":
                ServerTCP sTCP = new();
                break;
            case "4":
                ServerUDP sUDP = new();
                break;

        }
    }



}