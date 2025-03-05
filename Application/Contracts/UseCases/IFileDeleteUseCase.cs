namespace Application.Contracts.UseCases;

public interface IFileDeleteUseCase
{
    /// <summary>
    /// Executes the process to delete a file identified by the given ID.
    /// </summary>
    /// <param name="id">The unique identifier of the file to be deleted.</param>
    Task ExecuteAsync(int id);
}