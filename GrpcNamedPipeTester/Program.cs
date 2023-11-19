var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<NamedPipesConnectionFactory>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
