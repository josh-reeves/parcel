using System.Runtime.CompilerServices;

namespace PARCEL.Helpers;

public static class DebugLogger
{
    const string textToPrepend = "[DEBUG] ";

    static DebugLogger() { }

    public static void Log(object message, [CallerMemberName] string callerMemberName = "") 
    {
        Console.WriteLine(textToPrepend + callerMemberName + ' ' + (string)message);

    }

    public static void Log(object[] messages, [CallerMemberName] string callerMemberName = "")
    {
        foreach (object message in messages)
        {
            Console.WriteLine(textToPrepend + callerMemberName + ' ' + (string)message);

        }

    }

}
