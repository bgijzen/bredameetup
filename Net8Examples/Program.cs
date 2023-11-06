var builder = WebApplication.CreateBuilder(args);

#region  timeprovider

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddHostedService<TimeProviderExample>();

#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
