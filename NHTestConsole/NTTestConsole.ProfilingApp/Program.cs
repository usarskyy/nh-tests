using System;

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
      SimpleNHDealTester.TestStatelessHunderts(false, 1, false);
      SimpleNHDealTester.TestStatelessHunderts(false, 1, false);

      /*
      Console.WriteLine("Ended at: " + DateTime.UtcNow);
      Console.ReadLine();
      */
    }
  }
}
