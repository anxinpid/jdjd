using StackExchange.Redis;

namespace WebApp01
{
    public static class RedisCacheMiddleware
    {
        public static void AddStackExchangeRedisCacheSetup(this IServiceCollection services)
        {
            //switch (AppSettings.app(new string[] { "Startup", "Cache", "CacheType" }))
            //{
            //    case "Memory":
            //        services.AddDistributedMemoryCache();
            //        services.AddTransient<ICache, SystemCache>();
            //        break;
            //    case "Redis":
            //        services.AddSingleton<IDatabase>(sp =>
            //        {
            //            return ConnectionMultiplexer.Connect(AppSettings.app(new string[] { "Startup", "Cache", "RedisEndpoint" }))
            //            .GetDatabase(1);
            //        });
            //        services.AddTransient<ICache, RedisCache>();
            //        break;
            //    default: throw new Exception("缓存类型无效");
            //}

            services.AddSingleton<IDatabase>(sp =>
            {
                return ConnectionMultiplexer.Connect("XXXXX:6379,password=XXXXX,allowadmin=true")
                .GetDatabase(1);
            });
            services.AddTransient<ICache, RedisCache>();

        }
    }
}
