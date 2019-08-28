using System;

namespace CMTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var testIt = new TestIt();    
            if (args.Length >= 2 && args[0].Trim().Equals("-d", StringComparison.CurrentCultureIgnoreCase))
            {
                var args1 = args[1].Trim();
                if (!args1.Equals(""))
                {
                    //TestIt.RunDirectly _RunDirectly = new TestIt.RunDirectly() { run = true, device = args1 };
                    testIt.RunDirectly_Flow_PlugInOutServer(args1);
                }
            }
            else
            {
                testIt.OpenCmd();
            }
            Console.ReadKey();
        }
    }
}
