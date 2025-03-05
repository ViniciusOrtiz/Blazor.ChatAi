namespace Application.Contracts.UseCases;

public interface IAiAskQuestionUseCase
{
    Task<string> ExecuteAsync(string question);
}