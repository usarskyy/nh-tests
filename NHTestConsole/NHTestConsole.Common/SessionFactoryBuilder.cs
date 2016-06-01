using FluentNHibernate.Cfg;

using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Caches.Redis;
using NHibernate.Caches.RtMemoryCache;
using NHibernate.Caches.SysCache2;
using NHibernate.Cfg;
using NHibernate.Dialect;


namespace NHTestConsole.Common
{
  public static class SessionFactoryBuilder
  {
    public static ISessionFactory BuildWithMappingsFromAssemblyOf<TType>(CacheType cacheType)
    {
      var nhibernateCfg = new Configuration();

      if (cacheType == CacheType.Redis || cacheType == CacheType.RedisJson)
      {
        RedisConnectionMultiplexerInitializer.Init(cacheType == CacheType.RedisJson);
      }

      nhibernateCfg.SessionFactory()
                   .Proxy.Through<DefaultProxyFactoryFactory>()
                   .Integrate.Using<MsSql2008Dialect>()
                   .Connected.ByAppConfing("adminDb");

      var fluentCfg = Fluently.Configure(nhibernateCfg);

      fluentCfg.Cache(x =>
      {
        x.UseQueryCache();
        x.UseSecondLevelCache();

        switch (cacheType)
        {
          case CacheType.Redis:
          case CacheType.RedisJson:
            x.ProviderClass<RedisCacheProvider>();
            break;
          case CacheType.RtCache:
            x.ProviderClass<RtMemoryCacheProvider>();
            break;
          case CacheType.SysCache:
            x.ProviderClass<SysCacheProvider>();
            break;
        }
      });
      fluentCfg.Mappings(map => map.FluentMappings.AddFromAssemblyOf<TType>());

      return fluentCfg.BuildSessionFactory();
    }
  }
}