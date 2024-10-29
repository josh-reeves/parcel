using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PARCEL.Helpers;

public static class DebugLogger
{
    const string textToPrepend = "[DEBUG] ";

    static DebugLogger() { }

    public static void Log(object message, [CallerMemberName] string callerMemberName = "",[CallerLineNumber] int lineNumber = 0) 
    {
        Trace.WriteLine(textToPrepend + callerMemberName + ' ' + lineNumber + ' ' + ": " + message);

    }

    public static void Log(object[] messages, [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int lineNumber = 0)
    {

        foreach (object message in messages)
        {
            Trace.WriteLine(textToPrepend + callerMemberName + ' ' + lineNumber + ' ' + ": " + message);

        }

    }

}
