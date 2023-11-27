using System.ComponentModel.DataAnnotations;

namespace MvcWebApi.TodoApi;

public record TodoItem
{
    [Range(1, int.MaxValue, MinimumIsExclusive = true, MaximumIsExclusive = true)]
    public required int Id { get; init; }
    [Length(4, 10, ErrorMessage = "Length of the title is at least 4 and at most 10")]
    [Required]
    public required string Title { get; init; }
    public DateOnly? DueBy { get; init; }
    public bool IsComplete { get; init; } = false;
    [AllowedValues("Homework", "Classwork",  ErrorMessage = "Do your homework now!")]
    public string Category { get; init; } = string.Empty;

    [Base64String]
    public string? Hash { get; init; }
}
