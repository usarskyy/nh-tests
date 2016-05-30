using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Linq;

using NHTestConsole.DbSimpleCfg.Entities;


namespace NHTestConsole.DbSimpleCfg.Services
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
      var deals = GetBaseQuery(Constants.THOUSANDS_MERCHANT_ID).ToList();

      return deals;
    }

    public IList<DealDataEntity> LoadHunderts()
    {
      var deals = GetBaseQuery(Constants.HUNDERTS_MERCHANT_ID).ToList();

      return deals;
    }

    private IQueryable<DealDataEntity> GetBaseQuery(short dealTypeId)
    {
      var query = _dbSession.Query<DealDataEntity>()
                            .Where(x => x.DealTypeID == dealTypeId);

      if (_cachedQueries)
      {
        query = query.Cacheable();
      }

      return query;
    }
  }
}