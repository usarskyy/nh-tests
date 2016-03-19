using System.Collections.Generic;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg.Services
{
  public class AdoDealService : IDealService
  {
    public IList<DealDataEntity> LoadThousands()
    {
      var da = new ComplexDealDA();

      return da.LoadDeals(Constants.THOUSANDS_MERCHANT_ID);
    }

    public IList<DealDataEntity> LoadHunderts()
    {
      var da = new ComplexDealDA();

      return da.LoadDeals(Constants.HUNDERTS_MERCHANT_ID);
    }
  }
}