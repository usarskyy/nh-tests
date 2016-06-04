using System;
using System.Text;

using NHibernate;


namespace NHTestConsole.DbComplexCfg.Services
{
  public static class NHDealTester
  {
    public static void TestAdoHunderts(int cycles = 1, bool log = false)
    {
      var msg = new StringBuilder();
      var service = new AdoDealService();

      for (int i = 0; i < cycles; i++)
      {
        var result = service.LoadHunderts();

        if (log)
        {
          msg.AppendFormat("loaded " + result.Count + "; ");
        }
      }


      if (log)
      {
        Console.WriteLine("      " + msg);
      }
    }

    public static void TestStatefulHunderts(NHComplexConfigInitializer initializer, int cycles = 1, bool log = false)
    {
      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatefulSession(initializer))
        {
          var service = new NHStatefulDealService(session, initializer.Cached);
          var result = service.LoadHunderts();

          if (log)
          {
            Console.WriteLine("      loaded " + result.Count + "; ");
          }
        }
      }
    }

    public static void TestStatelessHunderts(NHComplexConfigInitializer initializer, int cycles = 1, bool log = false)
    {
      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatelessSession(initializer))
        {
          var service = new NHStatelessDealService(session, initializer.Cached);
          var result = service.LoadHunderts();
          
          if (log)
          {
            Console.WriteLine("      loaded " + result.Count + "; ");
          }
        }
      }
    }


    public static void TestAdoThousands(int cycles = 1, bool log = false)
    {
      var msg = new StringBuilder();
      var service = new AdoDealService();

      for (int i = 0; i < cycles; i++)
      {
        var result = service.LoadThousands();

        if (log)
        {
          msg.AppendFormat("loaded " + result.Count + "; ");
        }
      }

      if (log)
      {
        Console.WriteLine("      " + msg);
      }
    }

    public static void TestStatefulThousands(NHComplexConfigInitializer initializer, int cycles = 1, bool log = false)
    {
      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatefulSession(initializer))
        {
          var service = new NHStatefulDealService(session, initializer.Cached);
          var result = service.LoadThousands();
          
          if (log)
          {
            Console.WriteLine("      loaded " + result.Count + "; ");
          }
        }
      }
    }

    public static void TestStatelessThousands(NHComplexConfigInitializer initializer, int cycles = 1, bool log = false)
    {
      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatelessSession(initializer))
        {
          var service = new NHStatelessDealService(session, initializer.Cached);
          var result = service.LoadThousands();
          
          if (log)
          {
            Console.WriteLine("      loaded " + result.Count + "; ");
          }
        }
      }
    }

    
    private static ISession GetStatefulSession(NHComplexConfigInitializer initializer)
    {
      var session = initializer.SessionFactory.OpenSession();

      return session;
    }

    private static IStatelessSession GetStatelessSession(NHComplexConfigInitializer initializer)
    {
      var session = initializer.SessionFactory.OpenStatelessSession();

      return session;
    }
  }
}