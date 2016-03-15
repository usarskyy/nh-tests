using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Linq;

using NHTestConsole.DbSimpleCfg.Entities;


namespace NHTestConsole.DbSimpleCfg.Services
{
  public class NHStatefulDealService : IDealService
  {
    private readonly bool _cachedQueries;
    private readonly ISession _dbSession;

    public NHStatefulDealService(ISession dbSession, bool cachedQueries)
    {
      _cachedQueries = cachedQueries;
      _dbSession = dbSession;
    }


    public IList<DealDataEntity> LoadThousands()
    {
      var query = _dbSession.Query<DealDataEntity>()
                            .Where(x => x.MerchantID == Constants.THOUSANDS_MERCHANT_ID);

      if (_cachedQueries)
      {
        query = query.Cacheable();
      }

      var deals = query.ToList();

      return deals;
    }

    public IList<DealDataEntity> LoadHunderts()
    {
      var query = _dbSession.Query<DealDataEntity>()
                            .Where(x => x.MerchantID == Constants.HUNDERTS_MERCHANT_ID);

      if (_cachedQueries)
      {
        query = query.Cacheable();
      }

      var deals = query.ToList();

      return deals;
    }
  }
}