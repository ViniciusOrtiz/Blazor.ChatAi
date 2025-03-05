namespace Application.Contracts.Gateways;

public interface IAiGateway
{
    Task<float[]> GenerateEmbedding(string text);
    Task<string> GetAnswerFromAI(string question);
}