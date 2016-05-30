using NHibernate.Caches.Redis;

using StackExchange.Redis;


namespace NHTestConsole.Common
{
  public class RedisConnectionMultiplexerInitializer
  {
    private static bool _redisConnMultiplexerInitialized = false;

    public static void Init()
    {
      if (!_redisConnMultiplexerInitialized)
      {
        var connectionMultiplexer = ConnectionMultiplexer.Connect($"{RedisConst.SERVER_IP}:6379");
        RedisCacheProvider.SetConnectionMultiplexer(connectionMultiplexer);

        _redisConnMultiplexerInitialized = true;
      }
    }
  }
}