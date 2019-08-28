using ATLib.Invoke;
using System;

namespace ATLib
{
    class Program
    {
        static void Main(string[] args)
        {
            var invoker = new Invoker(args);
            Console.Write(invoker.HandleEvent());
            //System.Environment.Exit(System.Environment.ExitCode);
            Console.ReadKey();
        }
    }
}
