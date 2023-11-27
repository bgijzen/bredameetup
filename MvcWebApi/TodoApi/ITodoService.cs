namespace MvcWebApi.TodoApi;

public interface ITodoService
{
    IEnumerable<TodoItem> GetItems();
    void AddItem(TodoItem item);
}
