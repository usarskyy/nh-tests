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

        msg.AppendFormat("loaded " + result.Count + "; ");
      }

      if (log)
      {
        Console.WriteLine("      " + msg);
      }
    }

    public static void TestStatefulHunderts(CacheType cacheType, int cycles = 1, bool log = false)
    {
      var initializer = new NHSimpleConfigInitializer(cacheType);

      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatefulSession(initializer))
        {
          var msg = new StringBuilder();
          var service = new NHStatefulDealService(session, cacheType != CacheType.None);
          var result = service.LoadHunderts();

          msg.AppendFormat("loaded " + result.Count + "; ");
          
          if (log)
          {
            Console.WriteLine("      " + msg);
          }
        }
      }
    }

    public static void TestStatelessHunderts(CacheType cacheType, int cycles = 1, bool log = false)
    {
      var initializer = new NHSimpleConfigInitializer(cacheType);

      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatelessSession(initializer))
        {
          var msg = new StringBuilder();
          var service = new NHStatelessDealService(session, cacheType != CacheType.None);
          var result = service.LoadHunderts();

          msg.AppendFormat("loaded " + result.Count + "; ");
          
          if (log)
          {
            Console.WriteLine("      " + msg);
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

    public static void TestStatefulThousands(CacheType cacheType, int cycles = 1, bool log = false)
    {
      var initializer = new NHSimpleConfigInitializer(cacheType);

      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatefulSession(initializer))
        {
          var msg = new StringBuilder();
          var service = new NHStatefulDealService(session, cacheType != CacheType.None);
          var result = service.LoadThousands();

          msg.AppendFormat("loaded " + result.Count + "; ");
          
          if (log)
          {
            Console.WriteLine("      " + msg);
          }
        }
      }
    }

    public static void TestStatelessThousands(CacheType cacheType, int cycles = 1, bool log = false)
    {
      var initializer = new NHSimpleConfigInitializer(cacheType);

      for (int i = 0; i < cycles; i++)
      {
        using (var session = GetStatelessSession(initializer))
        {
          var msg = new StringBuilder();
          var service = new NHStatelessDealService(session, cacheType != CacheType.None);
          var result = service.LoadThousands();

          msg.AppendFormat("loaded " + result.Count + "; ");
          
          if (log)
          {
            Console.WriteLine("      " + msg);
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