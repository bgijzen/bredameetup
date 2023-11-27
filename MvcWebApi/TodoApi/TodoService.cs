namespace MvcWebApi.TodoApi;

public class TodoService : ITodoService
{
    private readonly IDictionary<int, TodoItem> _todoItems;

    public TodoService()
    {
        this._todoItems = LoadItems().ToDictionary(key => key.Id, value => value);
    }

    public IEnumerable<TodoItem> GetItems()
    {
        return this._todoItems.Values;
    }

    public void AddItem(TodoItem item)
    {
        this._todoItems.Add(item.Id, item);
    }

    private static TodoItem[] LoadItems()
    {
        var createItem = (int id, DateOnly? dueDate = null, string category = "Homework", string title = "Todo Item") => new TodoItem { Id = id, DueBy = dueDate, Category = category, Title = title + " " + id };

        return [
            createItem(1),
            createItem(2, DateOnly.FromDateTime(DateTime.Now.AddDays(4))),
            createItem(3, DateOnly.FromDateTime(DateTime.Now.AddDays(5))),
            createItem(4, DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
        ];
    }
}
