var builder = WebApplication.CreateBuilder(args);

#region  timeprovider

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddHostedService<TimeProviderExample>();

#endregion

builder.Services.AddHostedService<HostedLifecycleUpdatesUsingDependencyInjection>();
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.ListenNamedPipe("HelloWorld", listenOptions => {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
    serverOptions.Listen(IPAddress.Loopback, 7001, listenOptions => {
        listenOptions.UseHttps();
    });
    serverOptions.Listen(IPAddress.Loopback, 5291);
});

builder.Services.AddOpenTelemetry()
    .WithMetrics(openTelemetryBuilder =>
    {
        openTelemetryBuilder.AddPrometheusExporter();
        openTelemetryBuilder.AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel");

        openTelemetryBuilder.AddView("http.server.request.duration",
            new ExplicitBucketHistogramConfiguration
            {
                Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
            });
    });



var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGrpcService<GreeterService>();

app.MapPrometheusScrapingEndpoint();

app.Run();
