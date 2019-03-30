using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Web;
using StackExchange.Redis;

namespace CommentSold.WebTest
{
    public class Program
    {
       // private static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();

        //private static void InitializeConfiguration()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .AddUserSecrets<Program>();

        //    Configuration = builder.Build();
        //}

        //private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //{
        //    string cacheConnection = "commentsoldwebtest.redis.cache.windows.net:6380,password = HHCGnjFssaNalafrK0tOQMlF51rGxumqlBGuH4bSfZY =,ssl = True,abortConnect = False"
        //    return ConnectionMultiplexer.Connect(cacheConnection);
        //});

        //public static ConnectionMultiplexer Connection => lazyConnection.Value;
    }
}
