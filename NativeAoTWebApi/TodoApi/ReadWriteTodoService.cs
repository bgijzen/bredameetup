using System.Collections.Frozen;

namespace NativeAoTWebApi.TodoApi;

public class ReadWriteTodoService : ITodoService
{
    private readonly IDictionary<int, TodoItem> _todoItems;

    public ReadWriteTodoService()
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

    private static TodoItem[] LoadItems() => new[] {
        new TodoItem(1, "Item 1"),
        new(2, "Item 2", DateOnly.FromDateTime(DateTime.Now.AddDays(4))),
        new(3, "Item 3", DateOnly.FromDateTime(DateTime.Now.AddDays(2))),
        new(4, "Item 4", DateOnly.FromDateTime(DateTime.Now.AddDays(1)))
    };
}
