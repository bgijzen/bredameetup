namespace NativeAoTWebApi.TodoApi;

public record TodoItem(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);
