using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Linq;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg.Services
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
      using (_dbSession.BeginTransaction())
      {
        var deals = GetBaseQuery(Constants.THOUSANDS_MERCHANT_ID)
          .ToList();

        return deals;
      }
    }

    public IList<DealDataEntity> LoadHunderts()
    {
      using (_dbSession.BeginTransaction())
      {
        var deals = GetBaseQuery(Constants.HUNDERTS_MERCHANT_ID)
          .ToList();

        return deals;
      }
    }

    private IEnumerable<DealDataEntity> GetBaseQuery(short dealTypeId)
    {
      var query = _dbSession.Query<DealDataEntity>()
                            .Fetch(x => x.Application)
                            .Fetch(x => x.Merchant)
                            .Where(x => x.DealTypeID == dealTypeId);

      if (_cachedQueries)
      {
        query = query.Cacheable();
      }

      return query;
    }
  }
}