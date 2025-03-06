using Application.Contracts.Gateways;
using Application.Contracts.UseCases;

namespace Application.UseCases;

public sealed class AiAskQuestionUseCase : IAiAskQuestionUseCase
{
    private readonly IAiGateway _aiGateway;

    public AiAskQuestionUseCase(
        IAiGateway aiGateway)
    {
        _aiGateway = aiGateway;
    }

    /// <summary>
    /// Executes the process of asking a question to an AI system and retrieves its response.
    /// </summary>
    /// <param name="question">The question string to be sent to the AI system for processing.</param>
    /// <returns>A task representing the asynchronous operation, which returns the AI's response as a string.</returns>
    public async Task<string> ExecuteAsync(string question)
    {
        var response = await _aiGateway.GetAnswerFromAI(question);
        
        return response;
    }
}