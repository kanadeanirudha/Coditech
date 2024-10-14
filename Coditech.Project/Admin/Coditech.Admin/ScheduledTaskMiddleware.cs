namespace Coditech.Admin.Helpers
{
    public class ScheduledTaskMiddleware : BackgroundService
    {
        private readonly ILogger<ScheduledTaskMiddleware> _logger;
        private readonly TimeSpan _delay = TimeSpan.FromDays(1);

        public ScheduledTaskMiddleware(ILogger<ScheduledTaskMiddleware> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Your scheduled task
                _logger.LogInformation("Scheduled task is running at: {time}", DateTimeOffset.Now);

                // Delay for the next run
                await Task.Delay(_delay, stoppingToken);
            }
        }
    }
}
