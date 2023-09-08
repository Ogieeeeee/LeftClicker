using System.Runtime.InteropServices;

public static class Win32Helper
{
    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);
}