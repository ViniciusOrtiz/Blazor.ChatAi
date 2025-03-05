namespace Application.Models.Dtos.Message;

public class MessageInputDto
{
    public required string Text { get; set; }
    public required bool IsUser { get; set; }
    public string Alignment => IsUser ? "justify-content-end" : "justify-content-start";

}