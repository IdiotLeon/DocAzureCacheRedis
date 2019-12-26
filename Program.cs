using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;

namespace CoreAzureCacheRedis
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        const string SecretName = "CacheConnection";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
            Configuration = builder.Build();
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = Configuration[SecretName];
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
