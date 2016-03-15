using System;
using System.Text;

using NHibernate;

using NHTestConsole.DbSimpleCfg;
using NHTestConsole.DbSimpleCfg.Services;


namespace NHTestConsole.SimpleServices
{
  public static class NHDealTester
  {
    public static void TestAdoHunderts(int cycles = 1, bool log = false)
    {
      var msg = new StringBuilder();
      var service = new AdoDealService();

      for (int i = 1; i < cycles; i++)
      {
        var result = service.LoadHunderts();

        msg.AppendFormat("loaded " + result.Count + "; ");
      }

      if (log)
      {
        Console.WriteLine("      " + msg);
      }
    }

    public static void TestStatefulHunderts(bool useCache, int cycles = 1, bool log = false)
    {
      using (var session = GetStatefulSession(useCache))
      {
        var msg = new StringBuilder();

        for (int i = 1; i < cycles; i++)
        {
          var service = new NHStatefulDealService(session, useCache);
          var result = service.LoadHunderts();

          msg.AppendFormat("loaded " + result.Count + "; ");
        }

        if (log)
        {
          Console.WriteLine("      " + msg);
        }
      }
    }

    public static void TestStatelessHunderts(bool useCache, int cycles = 1, bool log = false)
    {
      using (var session = GetStatelessSession(useCache))
      {
        StringBuilder msg = new StringBuilder();

        for (int i = 1; i < cycles; i++)
        {
          var service = new NHStatelessDealService(session, useCache);
          var result = service.LoadHunderts();

          msg.AppendFormat("loaded " + result.Count + "; ");
        }

        if (log)
        {
          Console.WriteLine("      " + msg);
        }
      }
    }


    public static void TestAdoThousands(int cycles = 1, bool log = false)
    {
      var msg = new StringBuilder();
      var service = new AdoDealService();

      for (int i = 1; i < cycles; i++)
      {
        var result = service.LoadThousands();

        msg.AppendFormat("loaded " + result.Count + "; ");
      }

      if (log)
      {
        Console.WriteLine("      " + msg);
      }
    }

    public static void TestStatefulThousands(bool useCache, int cycles = 1, bool log = false)
    {
      using (var session = GetStatefulSession(useCache))
      {
        StringBuilder msg = new StringBuilder();

        for (int i = 0; i < cycles; i++)
        {
          var service = new NHStatefulDealService(session, useCache);
          var result = service.LoadThousands();

          msg.AppendFormat("loaded " + result.Count + "; ");
        }

        if (log)
        {
          Console.WriteLine("      " + msg);
        }
      }
    }

    public static void TestStatelessThousands(bool useCache, int cycles = 1, bool log = false)
    {
      using (var session = GetStatelessSession(useCache))
      {
        StringBuilder msg = new StringBuilder();

        for (int i = 0; i < cycles; i++)
        {
          var service = new NHStatelessDealService(session, useCache);
          var result = service.LoadThousands();

          msg.AppendFormat("loaded " + result.Count + "; ");
        }

        if (log)
        {
          Console.WriteLine("      " + msg);
        }
      }
    }

    
    private static ISession GetStatefulSession(bool useCache)
    {
      var initializer = new NHConfigInitializer(useCache);
      var session = initializer.SessionFactory.OpenSession();

      return session;
    }

    private static IStatelessSession GetStatelessSession(bool useCache)
    {
      var initializer = new NHConfigInitializer(useCache);
      var session = initializer.SessionFactory.OpenStatelessSession();

      return session;
    }
  }
}