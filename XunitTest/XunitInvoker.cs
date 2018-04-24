using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Runners;

namespace XunitTest
{
    public class XunitInvoker
    {
        static object consoleLock = new object();
        static ManualResetEvent finished = new ManualResetEvent(false);
        static int result = 0;
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("usage: TestRunner <assembly> [typeName]");
            }
            //var testAssembly = args[0];
            var testAssembly = @"D:\Dev\VS2015\TryXunit\TryXunit\bin\Debug\TryXunit.exe";
            var typeName = args.Length == 2 ? args[1] : null;
            using (var runner = AssemblyRunner.WithAppDomain(testAssembly))
            {
                runner.OnDiscoveryComplete = OnDiscoveryComplete;
                runner.OnExecutionComplete = OnExecutionComplete;
                runner.OnTestFailed = OnTestFailed;
                runner.OnTestSkipped = OnTestSkipped;
                runner.OnTestFinished = OnTestFinished;
                runner.OnTestPassed = OnTestPassed;
                Console.WriteLine("Discovering...");
                runner.Start(typeName);
                finished.WaitOne();
                finished.Dispose();
                Console.WriteLine(result);
            }
        }
        static void OnErrorMessage(DiscoveryCompleteInfo info)
        {
            lock (consoleLock)
                Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
        }
        static void OnDiscoveryComplete(DiscoveryCompleteInfo info)
        {
            lock (consoleLock)
                Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
        }
        static void OnExecutionComplete(ExecutionCompleteInfo info)
        {
            lock (consoleLock)
                Console.WriteLine($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");

            finished.Set();
        }
        static void OnTestFailed(TestFailedInfo info)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("[FAIL] {0}: {1}", info.TestDisplayName, info.ExceptionMessage);
                if (info.ExceptionStackTrace != null)
                    Console.WriteLine(info.ExceptionStackTrace);

                Console.ResetColor();
            }
            result = 1;
        }
        static void OnTestPassed(TestPassedInfo info)
        {
            lock (consoleLock)
            {

            }

            result = 1;
        }
        static void OnTestSkipped(TestSkippedInfo info)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason);
                Console.ResetColor();
            }
        }
        static void OnTestFinished(TestFinishedInfo info)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.ResetColor();
            }
        }
    }
}
