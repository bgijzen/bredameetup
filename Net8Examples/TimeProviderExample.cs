namespace Net8Examples;

public class TimeProviderExample(TimeProvider timeProvider, ILogger<TimeProviderExample> logger) : IHostedService {

    public int NumberOfCalls { get; private set; } = 0;
    private ITimer? _timer;

    private void Callback(object? state) {
        logger.LogInformation("Called");
        NumberOfCalls++;
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        timeProvider.CreateTimer(Callback, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken) {
        await _timer!.DisposeAsync();
    }
}
