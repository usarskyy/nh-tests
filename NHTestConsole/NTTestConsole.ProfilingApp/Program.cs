using System;

using NHTestConsole.Common;

using ComplexNHDealTester = NHTestConsole.DbComplexCfg.Services.NHDealTester;
using SimpleNHDealTester = NHTestConsole.DbSimpleCfg.Services.NHDealTester;


namespace NTTestConsole.ProfilingApp
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
      Console.WriteLine("Press ENTER to continue...");
      Console.ReadLine();
      Console.WriteLine("Started at: " + DateTime.UtcNow);
      */
      //ComplexNHDealTester.TestAdoHunderts(2, false);
      SimpleNHDealTester.TestStatefulThousands(CacheType.Redis, 1, false);
      SimpleNHDealTester.TestStatefulThousands(CacheType.Redis, 1, false);

      /*
      Console.WriteLine("Ended at: " + DateTime.UtcNow);
      Console.ReadLine();
      */
    }
  }
}
