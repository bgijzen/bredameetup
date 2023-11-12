var builder = WebApplication.CreateBuilder(args);

#region  timeprovider

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddHostedService<TimeProviderExample>();

#endregion

builder.Services.AddHostedService<HostedLifecycleUpdatesUsingDependencyInjection>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGrpcService<GreeterService>();
app.Run();
