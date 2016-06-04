using System;

using NHTestConsole.Common;
using NHTestConsole.DbComplexCfg;
using NHTestConsole.DbComplexCfg.Services;


namespace NHTestConsole
{
  internal static class ComplexCfgTester
  {
    public static void Test(CacheType cacheType)
    {
      const int testerReloadCount = 10;
      const int scenarioRerunCount = 1;
			var initializerNoCache = new NHComplexConfigInitializer();
			var initializerCache = new NHComplexConfigInitializer(cacheType);

			Console.WriteLine("Complex simple services: ");
      Console.WriteLine();
      
      ScenarioTester.Scenario($"Test ADO.NET without cache, loads a few hunderts rows {testerReloadCount} times", 
				() => NHDealTester.TestAdoHunderts(testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

			ScenarioTester.Scenario($"Test stateful session without cache, loads a few hunderts rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulHunderts(initializerNoCache, testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateless session without cache, loads a few hunderts rows {testerReloadCount} times", 
				() => NHDealTester.TestStatelessHunderts(initializerNoCache, testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateful session with cache [{cacheType}], loads a few hunderts rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulHunderts(initializerCache, testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);
			
      Console.WriteLine("-----------------------");
      
      ScenarioTester.Scenario($"Test ADO.NET without cache, loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestAdoThousands(testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateful session without cache, loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulThousands(initializerNoCache, testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateless session without cache, loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestStatelessThousands(initializerNoCache, testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateful session with cache [{cacheType}], loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestStatefulThousands(initializerCache, testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);

      ScenarioTester.Scenario($"Test stateless session with cache [{cacheType}], loads a few thousands rows {testerReloadCount} times", 
				() => NHDealTester.TestStatelessThousands(initializerCache, testerReloadCount), 
				runCycles: scenarioRerunCount, 
				warmup: true);
    } 
  }
}