using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using JIigor.Projects.TablePurger.Database;

namespace JIigor.Projects.TablePurger.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly PurgerService _purgerService;

        public Worker(ILogger<Worker> logger, PurgerService purgerService)
        {
            _logger = logger;
            _purgerService = purgerService;
        }

        private int _purgedRecords;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Checking database: {time}", DateTimeOffset.Now);
                _purgedRecords = await _purgerService.PurgeRecordAsync(default, stoppingToken);
                if (_purgedRecords != 0)
                {
                    _logger.LogInformation($"{_purgedRecords} records where purged at {DateTimeOffset.Now}");
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The purge service were stopped at {DateTimeOffset.Now}");
            return base.StopAsync(cancellationToken);
        }
    }
}
