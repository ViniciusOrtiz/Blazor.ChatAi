using Application.Contracts.Gateways;
using Application.Contracts.UseCases;

namespace Application.UseCases;

public class AiAskQuestionUseCase : IAiAskQuestionUseCase
{
    private readonly IAiGateway _aiGateway;

    public AiAskQuestionUseCase(
        IAiGateway aiGateway)
    {
        _aiGateway = aiGateway;
    }
    
    public async Task<string> ExecuteAsync(string question)
    {
        var response = await _aiGateway.GetAnswerFromAI(question);
        
        return response;
    }
}