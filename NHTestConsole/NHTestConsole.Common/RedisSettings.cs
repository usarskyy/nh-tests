using System.Configuration;


namespace NHTestConsole.Common
{
  public static class RedisSettings
  {
    public static string ServerHost => ConfigurationManager.AppSettings["redis-host"];
  }
}