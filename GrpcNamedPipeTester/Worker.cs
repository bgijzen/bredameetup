namespace GrpcNamedPipeTester;

public class  Worker : BackgroundService {
    private readonly NamedPipesConnectionFactory _namedPipesConnectionFactory;
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public Worker(NamedPipesConnectionFactory namedPipesConnectionFactory, ILogger<Worker> logger, IHostApplicationLifetime applicationLifetime) {
        _namedPipesConnectionFactory = namedPipesConnectionFactory;
        _logger = logger;
        _applicationLifetime = applicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        try {
            _namedPipesConnectionFactory.PipeName = "HelloWorld";
            var socketsHttpHandler =
                new SocketsHttpHandler { ConnectCallback = _namedPipesConnectionFactory.ConnectAsync };

            var channel = GrpcChannel.ForAddress("http://localhost",
                new GrpcChannelOptions { HttpHandler = socketsHttpHandler });
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
        finally {
            _applicationLifetime.StopApplication();
        }
    }
}
