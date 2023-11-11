using Grpc.Net.Client;

namespace GrpcTester;

public class  Worker : BackgroundService {
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger) {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            if (_logger.IsEnabled(LogLevel.Information)) {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(
                new HelloRequest { Name = "World" });

        }
    }
}
