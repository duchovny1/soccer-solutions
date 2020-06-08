namespace SoccerSolutionsApp.Web.HostedServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class ConsumeDataProcessingService : BackgroundService
    {
        private readonly ILogger<ConsumeDataProcessingService> logger;

        public ConsumeDataProcessingService(
            IServiceProvider services,
            ILogger<ConsumeDataProcessingService> logger)
        {
            this.Services = services;
            this.logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation(
                $"Consume Data Processing Hosted Service running...Started at {DateTime.UtcNow}");

            await this.DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            this.logger.LogInformation(
                   $"Consume Data Processing Hosted Service is working.");

            using (var scope = this.Services.CreateScope())
            {
                var dataProcessingService =
                    scope.ServiceProvider
                         .GetRequiredService<IDataProcessingService>();

                await dataProcessingService.DoWork(cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await Task.CompletedTask;
        }
    }
}
 