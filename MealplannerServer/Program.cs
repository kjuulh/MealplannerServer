using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rollbar;
using Rollbar.PlugIns.Serilog;
using Serilog;
using Serilog.Events;

namespace MealplannerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Define RollbarConfig:
            const string rollbarAccessToken = "9cd13d367e2b403d8cc9e972e99b58ee";
            const string rollbarEnvironment = "production";
            RollbarConfig rollbarConfig = new RollbarConfig(rollbarAccessToken)
            {
                Environment = rollbarEnvironment,
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RollbarSink(rollbarConfig)
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var port = Environment.GetEnvironmentVariable("PORT");

                if (!string.IsNullOrEmpty(port))
                {
                    var urls = new string[]
                    {
                        "http://*:" + port
                    };

                    webBuilder.UseUrls(urls);
                }

                webBuilder.UseStartup<Startup>().UseSerilog();
            });
    }
}