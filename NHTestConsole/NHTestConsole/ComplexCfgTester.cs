using System;

using NHTestConsole.DbComplexCfg.Services;


namespace NHTestConsole
{
  internal static class ComplexCfgTester
  {
    public static void Test()
    {
      const int testerReloadCount = 10;
      const int scenarioRerunCount = 1;

      Console.WriteLine("Complex simple services: ");
      Console.WriteLine();

      ScenarioTester.Scenario(string.Format("Test ADO.NET without cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestAdoHunderts(testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateful session without cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulHunderts(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateless session without cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatelessHunderts(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateful session with cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulHunderts(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateless session with cache, loads a few hunderts rows {0} times", testerReloadCount), () => NHDealTester.TestStatelessHunderts(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);

      Console.WriteLine("-----------------------");

      ScenarioTester.Scenario(string.Format("Test ADO.NET without cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestAdoThousands(testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateful session without cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulThousands(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateless session without cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatelessThousands(false, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateful session with cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulThousands(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario(string.Format("Test stateless session with cache, loads a few thousands rows {0} times", testerReloadCount), () => NHDealTester.TestStatefulThousands(true, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
    } 
  }
}