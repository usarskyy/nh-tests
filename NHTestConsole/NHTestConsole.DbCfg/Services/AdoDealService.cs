using System.Collections.Generic;

using NHTestConsole.DbSimpleCfg.Entities;


namespace NHTestConsole.DbSimpleCfg.Services
{
  public class AdoDealService : IDealService
  {
    public IList<DealDataEntity> LoadThousands()
    {
      var da = new DealDA();

      return da.LoadDeals(Constants.THOUSANDS_MERCHANT_ID);
    }

    public IList<DealDataEntity> LoadHunderts()
    {
      var da = new DealDA();

      return da.LoadDeals(Constants.HUNDERTS_MERCHANT_ID);
    }
  }
}