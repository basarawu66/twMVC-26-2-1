using StackExchange.Redis;

namespace twmvc26.Factory
{
    public interface IConnectionFactory
    {
        ConnectionMultiplexer GetConnection();
    }
}