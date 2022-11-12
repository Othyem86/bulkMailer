namespace BulkMailer.Services.Background;

public abstract class ScopedProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    protected ScopedProcessor(IServiceScopeFactory serviceScopeFactory) : base()
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected async Task Process()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            await ProcessInScope(scope.ServiceProvider);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            await Process();

            await Task.Delay(5000, stoppingToken); //5 seconds delay
        }
        while (!stoppingToken.IsCancellationRequested);
    }

    public abstract Task ProcessInScope(IServiceProvider serviceProvider);
}