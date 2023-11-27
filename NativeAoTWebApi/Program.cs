using System.Text.Json.Serialization;
using NativeAoTWebApi.TodoApi;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddKeyedScoped<ITodoService, TodoService>("readonly");
builder.Services.AddKeyedScoped<ITodoService, ReadWriteTodoService>("readwrite");

var app = builder.Build();

var todosApi = app.MapGroup("/todos");

todosApi.MapGet("/", ([FromKeyedServices("readonly")] ITodoService service) => service.GetItems());
todosApi.MapGet("/{id}", (int id, [FromKeyedServices("readonly")] ITodoService service) =>
    service.GetItems().FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

todosApi.MapPost("/", (TodoItem item, [FromKeyedServices("readwrite")] ITodoService service) => {
    service.AddItem(item);
    return Results.Ok(item);
});

app.Run();


[JsonSerializable(typeof(TodoItem[]))]
[JsonSerializable(typeof(IEnumerable<TodoItem>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
