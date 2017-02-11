using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using twmvc26.Models;
using twmvc26.Modules;
using twmvc26.Repositories;
using twmvc26.Services;

namespace twmvc26
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ProviderModule());

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<DataCacheService>().As<IDataCacheService>();
            builder.RegisterType<EmployeeService>().As<IService<Employee>>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}