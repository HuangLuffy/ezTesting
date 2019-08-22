using ATLib.Invoke;
using System;

namespace ATLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Invoker _Invoker = new Invoker(args);
            Console.Write(_Invoker.HandleEvent());
            //System.Environment.Exit(System.Environment.ExitCode);
            Console.ReadKey();
        }
    }
}
