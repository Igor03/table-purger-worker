using System;
using System.IO;
using JIigor.Projects.TablePurger.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JIigor.Projects.TablePurger.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
                        .AddPurgerDataContext(configuration)
                        .AddSingleton<PurgerService>();
                });
        }
            
    }
}
