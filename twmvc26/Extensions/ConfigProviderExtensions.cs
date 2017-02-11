using twmvc26.Providers.ConfigProvider;

namespace twmvc26.Extensions
{
    public static class ConfigProviderExtensions
    {
        /// <summary>
        /// 取得 Redis 連線字串
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string RedisConnection(this IConfigProvider config)
        {
            return config.Get("Redis:Connection", "localhost");
        }

        /// <summary>
        /// 取得 Redis 預設資料庫
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static int RedisDefaultDatabase(this IConfigProvider config)
        {
            return config.Get("Redis:DefaultDatabase", 2);
        }
    }
}