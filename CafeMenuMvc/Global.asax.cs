using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using CafeMenuMvc.Models.Profiles;
using CafeMenuMvc.Services.Abstract;
using CafeMenuMvc.Services.Concrete;
using System.Collections.Generic;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Redis;
using System.Configuration;

namespace CafeMenuMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(opt => opt.AddProfile<MappingProfile>());

            var builder = new ContainerBuilder();

            builder.RegisterInstance(ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["redisConnectionString"]))
                   .As<IConnectionMultiplexer>()
                   .SingleInstance();

            builder.Register(c => c.Resolve<ConnectionMultiplexer>().GetDatabase())
                   .As<IDatabase>();

            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().AsSelf().SingleInstance();
            builder.RegisterType<RabbitMQManager>().As<IRabbitMQService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionManager>().As<IEncryptionService>().AsSelf().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProductManager>().As<IProductService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DashboardManager>().As<IDashboardService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProductPropertyManager>().As<IProductPropertyService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TokenManager>().As<ITokenService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RedisCacheManager>().As<IRedisCacheService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<HttpManager>().As<IHttpService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                   .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var redisService = DependencyResolver.Current.GetService<IRedisCacheService>();
            var tokenService = DependencyResolver.Current.GetService<ITokenService>();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, redisService);
        }
    }
}
