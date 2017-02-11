using System;
using System.Configuration;
using Autofac;
using twmvc26.Providers.ConfigProvider;

namespace twmvc26.Modules
{
    public class ProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var cacheProviderType = ConfigurationManager.AppSettings.Get("CacheProviderType");

            builder
                .RegisterType(Type.GetType(cacheProviderType))
                .As<ICacheProvider>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ConfigProviderBase>()
                .As<IConfigProvider>()
                .InstancePerLifetimeScope();
        }
    }
}