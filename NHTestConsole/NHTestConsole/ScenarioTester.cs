using System;
using System.Diagnostics;


namespace NHTestConsole
{
  internal static class ScenarioTester
  {
    public static void Scenario(string name, Action action, int runCycles = 1, bool warmup = true)
    {
      Console.WriteLine("Scenario: {0}. {1} cycle(s) will be executed. Warmup is executed: {2}", name, runCycles, warmup);

      if (warmup)
      {
        // warm up
        action();
      }

      for (int i = 0; i < runCycles; i++)
      {
        GC.Collect();

        Stopwatch sw = new Stopwatch();
        sw.Start();
        action();
        sw.Stop();

        Console.WriteLine(" -> cycle #{0}: {1}", i, sw.Elapsed);
      }

      Console.WriteLine();
    } 
  }
}