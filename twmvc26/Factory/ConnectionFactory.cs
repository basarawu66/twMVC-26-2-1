using StackExchange.Redis;
using twmvc26.Extensions;
using twmvc26.Providers.ConfigProvider;

namespace twmvc26.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        private static readonly object _lockObject = new object();
        private ConnectionMultiplexer _connection;

        private IConfigProvider _configProvider;

        public ConnectionFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public ConnectionMultiplexer GetConnection()
        {
            if (_connection == null || _connection.IsConnected == false)
            {
                lock (_lockObject)
                {
                    if (_connection == null || _connection.IsConnected == false)
                    {
                        var connectionString = _configProvider.RedisConnection();
                        _connection = ConnectionMultiplexer.Connect(connectionString);
                    }
                }
            }

            return _connection;
        }
    }
}