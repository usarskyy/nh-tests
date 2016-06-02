using System.Configuration;


namespace NHTestConsole.Common
{
  public static class RedisSettings
  {
    public static string ConnectionString => ConfigurationManager.AppSettings["redis-conn-str"];
  }
}