using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;

        //Redis server ile haberleşecek class
        private ConnectionMultiplexer _redis;

        //Veritabanıyla haberleşecek ana sınıf
        public IDatabase db { get; set; }

        //appsettings deki datayı okumak için IConfiguration
        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }

        //Redis server'la haberleşme
        public void Connect()
        {
            var configString = $"{_redisHost}:{_redisPort}";
            _redis = ConnectionMultiplexer.Connect(configString);
        }

        //Veritabanı--istediğimiz db'yi seçebiliyoruz.db2 db3 gibi
        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }
    }
}