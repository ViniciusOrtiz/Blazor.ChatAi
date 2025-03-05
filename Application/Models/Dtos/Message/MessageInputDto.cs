namespace Application.Models.Dtos.Message;

public class MessageInputDto
{
    public required string Text { get; init; }
    public required bool IsUser { get; init; }
}