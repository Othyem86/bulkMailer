namespace BulkMailer.Services.Background;

public abstract class ScopedProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public int Delay = 5000;

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

            // TODO: get task delay value from some outside settings
            await Task.Delay(Delay, stoppingToken); 
        }
        while (!stoppingToken.IsCancellationRequested);
    }

    public abstract Task ProcessInScope(IServiceProvider serviceProvider);
}