using NHibernate;

using NHTestConsole.Common;
using NHTestConsole.DbSimpleCfg.Mappings;


namespace NHTestConsole.DbSimpleCfg
{
  public class NHSimpleConfigInitializer
  {
    public ISessionFactory SessionFactory { get; private set; }

		public bool Cached { get; }

    public NHSimpleConfigInitializer(CacheType cacheType = CacheType.None)
    {
	    Cached = cacheType != CacheType.None;
			SessionFactory = SessionFactoryBuilder.BuildWithMappingsFromAssemblyOf<DealMap>(cacheType);
    }
  }
}