using System;

using NHTestConsole.Common;
using NHTestConsole.DbSimpleCfg;
using NHTestConsole.DbSimpleCfg.Services;


namespace NHTestConsole
{
  internal static class SimpleCfgTester
  {
    public static void Test(CacheType cacheType)
    {
      const int testerReloadCount = 10;
      const int scenarioRerunCount = 1;
			var initializerNoCache = new NHSimpleConfigInitializer();
			var initializerCache = new NHSimpleConfigInitializer(cacheType);

			Console.WriteLine("Test simple services: ");
      Console.WriteLine();

			ScenarioTester.Scenario($"Test ADO.NET without cache, loads a few hunderts rows {testerReloadCount} times",
		    () => NHDealTester.TestAdoHunderts(testerReloadCount),
		    runCycles: scenarioRerunCount,
		    warmup: true);

      ScenarioTester.Scenario($"Test stateful session without cache, loads a few hunderts rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulHunderts(initializerNoCache, cycles: testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateless session without cache, loads a few hunderts rows {testerReloadCount} times", 
				() => NHDealTester.TestStatelessHunderts(initializerNoCache, cycles: testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateful session with cache [{cacheType}], loads a few hunderts rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulHunderts(initializerCache, cycles: testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);
			
      Console.WriteLine("-----------------------");

      ScenarioTester.Scenario($"Test ADO.NET without cache, loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestAdoThousands(testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateful session without cache, loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulThousands(initializerNoCache, cycles: testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateless session without cache, loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestStatelessThousands(initializerNoCache, cycles: testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateful session with cache [{cacheType}], loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulThousands(initializerCache, cycles: testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);
    }
  }
}