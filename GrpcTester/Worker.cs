using System.Diagnostics;
using Grpc.Net.Client;

namespace GrpcTester;

public class  Worker : BackgroundService {
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime applicationLifetime) {
        _logger = logger;
        _applicationLifetime = applicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        
        try {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://localhost:7001",
                new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Greeter.GreeterClient(channel);

            var stopwatch = Stopwatch.StartNew();
            int count = 0; 
            while (!stoppingToken.IsCancellationRequested && stopwatch.Elapsed.TotalSeconds <= 30) {
                var response = await client.SayHelloAsync(
                    new HelloRequest { Name = "World" });
                count++;

                _logger.LogDebug("Response: {Response}", response);
            }
            stopwatch.Stop();
            _logger.LogInformation("Processed {Count} in {Seconds} (Req/seq: {RequestsPerSecond}", 
                count, 
                stopwatch.Elapsed.TotalSeconds,
                count / stopwatch.Elapsed.TotalSeconds);
        }
        finally 
        {
            _applicationLifetime.StopApplication();
        }
    }
}
