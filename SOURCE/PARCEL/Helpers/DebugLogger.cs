using System.Runtime.CompilerServices;

namespace PARCEL.Helpers;

public static class DebugLogger
{
    const string textToPrepend = "[DEBUG] ";

    static DebugLogger() { }

    public static void Log(string message, [CallerMemberName] string callerMemberName = "") 
    {
        Console.WriteLine(textToPrepend + callerMemberName + ' ' + message);

    }

    public static void Log(string[] messages, [CallerMemberName] string callerMemberName = "")
    {
        foreach (string message in messages)
        {
            Console.WriteLine(textToPrepend + callerMemberName + ' ' + message);

        }

    }

}
