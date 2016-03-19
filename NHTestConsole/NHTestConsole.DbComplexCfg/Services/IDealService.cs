using System.Collections.Generic;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg.Services
{
  public interface IDealService
  {
    IList<DealDataEntity> LoadThousands();
    IList<DealDataEntity> LoadHunderts();
  }
}