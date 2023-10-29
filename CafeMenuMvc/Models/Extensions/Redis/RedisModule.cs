using Autofac;
using StackExchange.Redis;

namespace CafeMenuMvc.Models.Extensions.Redis
{
    public class RedisModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(ConnectionMultiplexer.Connect("localhost"))
                   .As<IConnectionMultiplexer>()
                   .SingleInstance();

            builder.Register(c => c.Resolve<ConnectionMultiplexer>().GetDatabase())
                   .As<IDatabase>();
        }
    }
}