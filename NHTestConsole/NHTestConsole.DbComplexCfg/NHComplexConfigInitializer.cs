using System;

using FluentNHibernate.Cfg;

using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;

using NHTestConsole.DbComplexCfg.Mappings;


namespace NHTestConsole.DbComplexCfg
{
  public class NHComplexConfigInitializer
  {
    public ISessionFactory SessionFactory { get; private set; }

    public NHComplexConfigInitializer(bool useCache = false)
    {
      var nhibernateCfg = new Configuration();

      nhibernateCfg.SessionFactory()
                   .Proxy.Through<DefaultProxyFactoryFactory>()
                   .Integrate.Using<MsSql2008Dialect>()
                   .Connected.ByAppConfing("adminDb");

      var fluentCfg = Fluently.Configure(nhibernateCfg.Cache(x =>
      {
        x.UseQueryCache = true;

        if (useCache)
        {
          x.Provider<NHibernate.Caches.RtMemoryCache.RtMemoryCacheProvider>();
          //x.Provider<NHibernate.Caches.SysCache2.SysCacheProvider>();
        }
      }));

      fluentCfg.Mappings(map => map.FluentMappings.AddFromAssemblyOf<DealMap>());

      SessionFactory = fluentCfg.BuildSessionFactory();
    }
  }
}