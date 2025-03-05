namespace Application.Models.Files;

public class DocumentModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required double Size { get; init; }
    public required DateTime CreatedAt { get; init; }
}