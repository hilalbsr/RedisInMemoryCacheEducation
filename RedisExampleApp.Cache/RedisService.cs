using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisExampleApp.Cache
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _connectionMultiplexer;

        public IDatabase db { get; set; }

        //public RedisService(IConfiguration configuration)
        //{
        //    _redisHost = configuration["Redis:Host"];
        //    _redisPort = configuration["Redis:Port"];
        //}

        public RedisService(string url)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url);

        }

        //public void Connect()
        //{
        //    var configString = $"{_redisHost}:{_redisPort}";
        //    _connectionMultiplexer = ConnectionMultiplexer.Connect(configString);
        //}

        public IDatabase GetDb(int db)
        {
            return _connectionMultiplexer.GetDatabase(db);
        }
    }
}