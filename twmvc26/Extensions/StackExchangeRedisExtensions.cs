using System;
using Microsoft.IO;
using MsgPack.Serialization;
using StackExchange.Redis;

namespace twmvc26.Extensions
{
    public static class StackExchangeRedisExtensions
    {
        private static readonly RecyclableMemoryStreamManager _memoryStreamManager = new RecyclableMemoryStreamManager();

        public static T Get<T>(this IDatabase cache, string key)
        {
            var value = cache.StringGet(key);

            if (!value.HasValue)
            {
                return default(T);
            }

            return Deserialize<T>(value);
        }

        public static void Set(this IDatabase cache, string key, object data, int cacheSeconds)
        {
            var expire = TimeSpan.FromSeconds(cacheSeconds);

            cache.Set(key, data, expire);
        }

        public static void Set(this IDatabase cache, string key, object data, TimeSpan? expire = null)
        {
            if (data == null)
            {
                return;
            }

            var buffer = Serialize(data);

            cache.StringSet(key, buffer, expire);
        }

        private static byte[] Serialize<T>(T o)
        {
            if (o == null)
            {
                return null;
            }

            var serializer = SerializationContext.Default.GetSerializer<T>(o);

            using (var stream = _memoryStreamManager.GetStream())
            {
                serializer.Pack(stream, o);
                return stream.ToArray();
            }
        }

        private static T Deserialize<T>(byte[] data)
        {
            if (data == null)
            {
                return default(T);
            }

            var serializer = SerializationContext.Default.GetSerializer<T>();

            using (var stream = _memoryStreamManager.GetStream())
            {
                return serializer.Unpack(stream);
            }
        }
    }
}