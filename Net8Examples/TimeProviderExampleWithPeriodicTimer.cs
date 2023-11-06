namespace Net8Examples;

public class TimeProviderExampleWithPeriodicTimer(TimeProvider timeProvider, ILogger<TimeProviderExampleWithPeriodicTimer> logger) : IHostedService {
    private PeriodicTimer? _periodicTimer;
    
    public int NumberOfCalls { get; private set; } = 0;
    
    public async Task StartAsync(CancellationToken cancellationToken) {
        _periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(1), timeProvider);
        Task.Yield();
        
        while (!cancellationToken.IsCancellationRequested &&
               await _periodicTimer.WaitForNextTickAsync(cancellationToken)) 
        {
            logger.LogInformation("Called");
            NumberOfCalls++;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        _periodicTimer!.Dispose();
        return Task.CompletedTask;
    }
}
