namespace LeftClicker.Helper;

public static class VirtualKeyHelper
{
    private static void PromptClickMode()
    {
        System.Console.WriteLine("Which mode would you like to chose");
        System.Console.WriteLine("1. YOLO");
        System.Console.WriteLine("2. League of Legends");
        System.Console.WriteLine("3. GAMING");
        System.Console.WriteLine("4. CODING");
        System.Console.WriteLine("5. SAFE");
    }

    public static ClickMode GetUserClickMode()
    {
        PromptClickMode();
        var userChoice = ClickMode.Safe;
        var isValidAnswer = false;

        while (!isValidAnswer)
        {
            if (Enum.TryParse(Console.ReadLine(), true, out userChoice))
            {
                isValidAnswer = true;
            }
        }

        return userChoice;
    }

    public static List<VKeys> AllowedKeys(ClickMode clickMode)
    {
        var allowedKeys = new List<VKeys>();

        if (clickMode == ClickMode.Yolo)
        {
            foreach (VKeys key in Enum.GetValues(typeof(VKeys)))
            {
                if (key == VKeys.MENU || key == VKeys.SHIFT || key == VKeys.CONTROL)
                    continue;
                allowedKeys.Add(key);
            }
        }

        if (clickMode == ClickMode.Coding)
        {
            allowedKeys = new(){
                    VKeys.KEY_A,
                    VKeys.KEY_E,
                    VKeys.KEY_I,
                    VKeys.KEY_O,
                    VKeys.KEY_U,
                    VKeys.RBUTTON,
                    VKeys.LBUTTON,
                    VKeys.MBUTTON,
                    VKeys.BROWSER_BACK,
                    VKeys.BROWSER_FORWARD
                };
        }

        if (clickMode == ClickMode.LeagueOfLegends)
        {
            allowedKeys = new(){
                    VKeys.KEY_Q
                };
        }

        if (clickMode == ClickMode.Gaming)
        {
            allowedKeys = new(){
                    VKeys.KEY_Q,
                    VKeys.KEY_W,
                    VKeys.KEY_E,
                    VKeys.KEY_R,
                    VKeys.KEY_A,
                    VKeys.KEY_S,
                    VKeys.KEY_D,
                    VKeys.LEFT,
                    VKeys.UP,
                    VKeys.DOWN,
                    VKeys.RIGHT,
                    VKeys.LBUTTON,
                    VKeys.RBUTTON,
                    VKeys.MBUTTON
                };
        }

        if (clickMode == ClickMode.Safe)
        {
            allowedKeys.Add(VKeys.LBUTTON);
        }

        return allowedKeys;
    }
}