using System;


namespace NHTestConsole
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      SimpleCfgTester.Test();
      Console.WriteLine();

      ComplexCfgTester.Test();
      Console.ReadLine();
    }
  }
}