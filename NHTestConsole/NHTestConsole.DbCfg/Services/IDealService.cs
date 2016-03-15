using System.Collections.Generic;

using NHTestConsole.DbSimpleCfg.Entities;


namespace NHTestConsole.DbSimpleCfg.Services
{
  public interface IDealService
  {
    IList<DealDataEntity> LoadThousands();
    IList<DealDataEntity> LoadHunderts();
  }
}