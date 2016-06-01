using NHibernate.Caches.Redis;

using StackExchange.Redis;


namespace NHTestConsole.Common
{
  public class RedisConnectionMultiplexerInitializer
  {
    private static bool _redisConnMultiplexerInitialized = false;

    public static void Init(bool useJsonSerializer)
    {
      if (!_redisConnMultiplexerInitialized)
      {
        var connectionMultiplexer = ConnectionMultiplexer.Connect($"{RedisSettings.ServerHost}:6379");
        RedisCacheProvider.SetConnectionMultiplexer(connectionMultiplexer);

        if (useJsonSerializer)
        {
          var options = new RedisCacheProviderOptions()
          {
            Serializer = new NhJsonCacheSerializer()
          };

          RedisCacheProvider.SetOptions(options);
        }

        _redisConnMultiplexerInitialized = true;
      }
    }
  }
}