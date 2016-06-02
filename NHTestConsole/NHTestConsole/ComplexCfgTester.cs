﻿using System;

using NHTestConsole.Common;
using NHTestConsole.DbComplexCfg.Services;


namespace NHTestConsole
{
  internal static class ComplexCfgTester
  {
    public static void Test(CacheType cacheType)
    {
      const int testerReloadCount = 10;
      const int scenarioRerunCount = 1;

      Console.WriteLine("Complex simple services: ");
      Console.WriteLine();
      
      ScenarioTester.Scenario($"Test ADO.NET without cache, loads a few hunderts rows {testerReloadCount} times", () => NHDealTester.TestAdoHunderts(testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateful session without cache, loads a few hunderts rows {testerReloadCount} times", () => NHDealTester.TestStatefulHunderts(CacheType.None, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateless session without cache, loads a few hunderts rows {testerReloadCount} times", () => NHDealTester.TestStatelessHunderts(CacheType.None, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateful session with cache [{cacheType}], loads a few hunderts rows {testerReloadCount} times", () => NHDealTester.TestStatefulHunderts(cacheType, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateless session with cache [{cacheType}], loads a few hunderts rows {testerReloadCount} times", () => NHDealTester.TestStatelessHunderts(cacheType, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);

      Console.WriteLine("-----------------------");
      
      ScenarioTester.Scenario($"Test ADO.NET without cache, loads a few thousands rows {testerReloadCount} times", () => NHDealTester.TestAdoThousands(testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateful session without cache, loads a few thousands rows {testerReloadCount} times", () => NHDealTester.TestStatefulThousands(CacheType.None, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateless session without cache, loads a few thousands rows {testerReloadCount} times", () => NHDealTester.TestStatelessThousands(CacheType.None, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateful session with cache [{cacheType}], loads a few thousands rows {testerReloadCount} times", () => NHDealTester.TestStatefulThousands(cacheType, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
      ScenarioTester.Scenario($"Test stateless session with cache [{cacheType}], loads a few thousands rows {testerReloadCount} times", () => NHDealTester.TestStatelessThousands(cacheType, testerReloadCount), runCycles: scenarioRerunCount, warmup: true);
    } 
  }
}