namespace Application.Contracts.UseCases;

public interface IAiAskQuestionUseCase
{
    /// <summary>
    /// Executes the process of sending a question to an AI system and retrieves its response asynchronously.
    /// </summary>
    /// <param name="question">The question to be processed by the AI system.</param>
    /// <returns>A task representing the asynchronous operation, which returns the AI's response as a string.</returns>
    Task<string> ExecuteAsync(string question);
}