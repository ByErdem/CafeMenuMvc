using StackExchange.Redis;
using System;

namespace CafeMenuMvc.Models.Extensions.Redis
{
    public class RedisConnectionFactory
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
            return ConnectionMultiplexer.Connect("localhost");
        });

        public static ConnectionMultiplexer Connection => lazyConnection.Value;
    }
}