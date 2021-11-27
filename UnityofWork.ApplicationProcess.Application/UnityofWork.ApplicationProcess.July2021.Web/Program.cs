using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using UnityofWork.ApplicationProcess.July2021.Data.Data;
using Serilog;
using Serilog.Formatting.Compact;
using UnityofWork.ApplicationProcess.July2021.Web.Configurations;


namespace UnityofWork.ApplicationProcess.July2021.Web
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

            //2. Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of BoardGamesDBContext in our services layer
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DBContext>();

                //4. Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            }

            //Continue to run the application
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
