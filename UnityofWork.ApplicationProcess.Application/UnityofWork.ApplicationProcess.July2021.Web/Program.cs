using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using Hahn.ApplicationProcess.July2021.Data.Data;
using Serilog;
using Serilog.Formatting.Compact;
using Hahn.ApplicationProcess.July2021.Web.Configurations;


namespace Hahn.ApplicationProcess.July2021.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

            LogFileConfiguration fileConfig = new LogFileConfiguration();
            config.GetSection("LogFile").Bind(fileConfig);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(new RenderedCompactJsonFormatter()).WriteTo.Debug(outputTemplate: DateTime.Now.ToString()).WriteTo.File(fileConfig.FileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            
            using (var scope = host.Services.CreateScope())
            {
                
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DBContext>();

               
                DataGenerator.Initialize(services);
            }

           
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logBuilder =>
            {
                logBuilder.AddFilter("System", LogLevel.Error).AddFilter("Microsoft", LogLevel.Information);
            })
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
