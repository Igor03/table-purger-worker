using System;
using System.IO;
using JIigor.Projects.TablePurger.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace JIigor.Projects.TablePurger.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Going back one directory... pretty cool, hun?!
            var logDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ".."));
            var logFileName = "PurgerLog.txt";

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.File(@$"{logDirectory}\{logFileName}")
                    .CreateLogger();    
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                Log.Fatal($"A problem occurred. See details {exception.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            const string machineUser = "jigor";
            const string secretsJsonId = "1104a8c7-ef98-49ec-a5a6-f1ddbd4863b6";
            var secretsJson =
                $"C:/Users/{machineUser}/AppData/Roaming/Microsoft/UserSecrets/{secretsJsonId}/secrets.json";


            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile(secretsJson, false)
                .Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>()
                        .AddSingleton<IConfiguration>(configuration)
                        .AddPurgerDataContext(configuration);
                })
                .UseSerilog();
        }
    }
}