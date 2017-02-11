using System;
using System.Configuration;

namespace twmvc26.Providers.ConfigProvider
{
    /// <summary>
    /// ConfigProviderBase
    /// </summary>
    /// <seealso cref="IConfigProvider" />
    public abstract class ConfigProviderBase : IConfigProvider
    {
        public virtual string Get(string key, string defaultValue = null)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        public T Get<T>(string key) where T : IConvertible
        {
            var value = this.Get(key);

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T Get<T>(string key, T defaultValue) where T : IConvertible
        {
            var value = this.Get(key);

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}