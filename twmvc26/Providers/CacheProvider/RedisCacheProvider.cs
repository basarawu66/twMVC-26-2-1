using System;
using StackExchange.Redis;
using twmvc26.Extensions;
using twmvc26.Factory;

namespace twmvc26
{
    public class RedisCacheProvider : ICacheProvider
    {
        public int DefaultDatabaseId { get; private set; } = 2;

        private static IConnectionFactory _connectionFactory;

        public RedisCacheProvider(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public RedisCacheProvider(IConnectionFactory connectionFactory, int defaultDatabaseId)
        {
            _connectionFactory = connectionFactory;
            DefaultDatabaseId = defaultDatabaseId;
        }

        private ConnectionMultiplexer Connection => _connectionFactory.GetConnection();

        private IDatabase DefaultDatabase => Connection.GetDatabase(DefaultDatabaseId);

        public IDatabase GetDatabase(int db)
        {
            return Connection.GetDatabase(db);
        }

        public T Get<T>(string key)
        {
            return DefaultDatabase.Get<T>(key);
        }

        public void Set(string key, object data, int cacheTime)
        {
            DefaultDatabase.Set(key, data, TimeSpan.FromSeconds(cacheTime));
        }

        public bool IsSet(string key)
        {
            return DefaultDatabase.KeyExists(key);
        }

        public void Remove(string key)
        {
            DefaultDatabase.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            foreach (var endPoint in Connection.GetEndPoints())
            {
                var server = Connection.GetServer(endPoint);

                foreach (var key in server.Keys(pattern: pattern))
                {
                    Remove(key);
                }
            }
        }

        public void Clear()
        {
            foreach (var endPoint in Connection.GetEndPoints())
            {
                var server = Connection.GetServer(endPoint);

                server.FlushDatabase();
            }
        }
    }
}