﻿using System;
using System.Text;

using NHibernate;

using NHTestConsole.Common;


namespace NHTestConsole.DbSimpleCfg.Services
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

    public static void TestStatefulHunderts(NHSimpleConfigInitializer initializer, int cycles = 1, bool log = false)
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

    public static void TestStatelessHunderts(NHSimpleConfigInitializer initializer, int cycles = 1, bool log = false)
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

        msg.AppendFormat("loaded " + result.Count + "; ");
      }

      if (log)
      {
        Console.WriteLine("      " + msg);
      }
    }

    public static void TestStatefulThousands(NHSimpleConfigInitializer initializer, int cycles = 1, bool log = false)
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

    public static void TestStatelessThousands(NHSimpleConfigInitializer initializer, int cycles = 1, bool log = false)
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

    
    private static ISession GetStatefulSession(NHSimpleConfigInitializer initializer)
    {
      var session = initializer.SessionFactory.OpenSession();

      return session;
    }

    private static IStatelessSession GetStatelessSession(NHSimpleConfigInitializer initializer)
    {
      var session = initializer.SessionFactory.OpenStatelessSession();

      return session;
    }
  }
}