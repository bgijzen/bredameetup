namespace Net8Examples;

public class HostedLifecycleUpdatesUsingDependencyInjection : IHostedService {
    private readonly ILogger<HostedLifecycleUpdatesUsingDependencyInjection> _logger;

    public HostedLifecycleUpdatesUsingDependencyInjection(IHostApplicationLifetime applicationLifetime, ILogger<HostedLifecycleUpdatesUsingDependencyInjection> logger) {
        _logger = logger;
        applicationLifetime.ApplicationStarted.Register(OnStarted);
        applicationLifetime.ApplicationStopping.Register(OnStopping);
        applicationLifetime.ApplicationStopped.Register(OnStopped);
    }

    private void OnStarted() {
        _logger.LogInformation("OnStarted");
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("StartAsync");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("StopAsync");
        return Task.CompletedTask;
    }
    
    private void OnStopping() {
        _logger.LogInformation("OnStopping");
    }

    private void OnStopped() {
        _logger.LogInformation("OnStopped");
    }
}

public class HostedLifecycleUpdatesUsingInterface (ILogger<HostedLifecycleUpdatesUsingInterface> logger) : IHostedLifecycleService {
    
    public Task StartAsync(CancellationToken cancellationToken) {
        logger.LogInformation("StartAsync");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        logger.LogInformation("StopAsync");
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken) {
        logger.LogInformation("StartedAsync");
        return Task.CompletedTask;
    }

    public Task StartingAsync(CancellationToken cancellationToken) {
        logger.LogInformation("StartingAsync");
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken) {
        logger.LogInformation("StoppedAsync");
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken) {
        logger.LogInformation("StoppingAsync");
        return Task.CompletedTask;
    }
}
