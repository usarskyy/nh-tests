using System;

using NHTestConsole.Common;


namespace NHTestConsole
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var cacheType = CacheType.RedisJson;

      SimpleCfgTester.Test(cacheType);
      Console.WriteLine();

      ComplexCfgTester.Test(cacheType);
      Console.ReadLine();
    }
  }
}