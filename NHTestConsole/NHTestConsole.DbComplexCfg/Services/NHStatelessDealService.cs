using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Linq;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg.Services
{
  public class NHStatelessDealService : IDealService
  {
    private readonly bool _cachedQueries;
    private readonly IStatelessSession _dbSession;

    public NHStatelessDealService(IStatelessSession dbSession, bool cachedQueries)
    {
      _cachedQueries = cachedQueries;
      _dbSession = dbSession;
    }


    public IList<DealDataEntity> LoadThousands()
    {
      var query = _dbSession.Query<DealDataEntity>()
                            .Where(x => x.DealTypeID == Constants.THOUSANDS_MERCHANT_ID);

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
                            .Where(x => x.DealTypeID == Constants.HUNDERTS_MERCHANT_ID);

      if (_cachedQueries)
      {
        query = query.Cacheable();
      }

      var deals = query.ToList();

      return deals;
    }
  }
}