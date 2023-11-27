using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcWebApi.TodoApi;

namespace MvcWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IHttpClientFactory _factory;
    private readonly ILogger<TodoController> _logger;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Because isn't the same")]
    public TodoController(ITodoService todoService, IHttpClientFactory factory, ILogger<TodoController> logger)
    {
        this._todoService = todoService;
        this._factory = factory;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<TodoItem[]> Get()
    {
        this._logger.LogInformation("Loading todo items");
        return this.Ok(this._todoService.GetItems());
    }

    [HttpPost]
    public ActionResult<TodoItem> Post([FromBody] TodoItem item)
    {
        this._todoService.AddItem(item);
        return this.Ok(item);
    }

    [HttpGet("external")]
    public async Task<ActionResult<TodoItem[]>> GetExternal()
    {
        const string uri = "https://meetupbreda.free.beeceptor.com/todos";
        using var httpClient = this._factory.CreateClient("TODO client");

        IAsyncEnumerable<TodoItem?> items = httpClient.GetFromJsonAsAsyncEnumerable<TodoItem?>(uri);

        await foreach (var item in items) {
            this._logger.LogInformation("Got: {todoItem}", item?.Title);
        }

        return this.Ok();
    }
}
