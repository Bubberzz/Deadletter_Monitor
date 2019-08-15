using System;
using System.IO;

namespace Deadletter_Monitor
{
    public class Log
    {
        public static void log(string logMessage, TextWriter w)
        {
            w.Write("Log Entry : ");
            w.WriteLine($"{Environment.UserName} - {DateTime.Now.ToLongTimeString()} - {DateTime.Now.ToLongDateString()}");
            w.WriteLine($"{logMessage}");
            w.WriteLine("--------------------------------------------------------");
        }
    }
}