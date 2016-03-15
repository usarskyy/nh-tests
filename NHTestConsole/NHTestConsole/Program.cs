using System;
using System.Diagnostics;

using NHTestConsole.SimpleServices;


namespace NHTestConsole
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      const int testerReloadCount = 10;
      const int scenarioRerunCount = 3;

      Scenario(string.Format("Test ADO.NET without cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestAdoHunderts(testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateful session without cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulHunderts(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateless session without cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatelessHunderts(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateful session with cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulHunderts(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateless session with cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatelessHunderts(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);

      Console.WriteLine("-----------------------");

      Scenario(string.Format("Test ADO.NET without cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestAdoThousands(testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateful session without cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulThousands(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateless session without cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatelessThousands(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateful session with cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulThousands(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);
      Scenario(string.Format("Test stateless session with cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulThousands(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: false);

      Console.ReadLine();
    }

    private static void Scenario(string name, Action action, int runCycles = 1, bool warmup = true)
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