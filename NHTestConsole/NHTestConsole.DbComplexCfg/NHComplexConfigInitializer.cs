using NHibernate;

using NHTestConsole.Common;
using NHTestConsole.DbComplexCfg.Mappings;


namespace NHTestConsole.DbComplexCfg
{
  public class NHComplexConfigInitializer
  {
    public ISessionFactory SessionFactory { get; private set; }

    public NHComplexConfigInitializer(CacheType cacheType = CacheType.None)
    {
      SessionFactory = SessionFactoryBuilder.BuildWithMappingsFromAssemblyOf<DealMap>(cacheType);
    }
  }
}