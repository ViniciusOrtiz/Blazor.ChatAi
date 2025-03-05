using Application.Models.Dtos.Files;

namespace Application.Contracts.UseCases;

public interface IFileGetContentUseCase
{
    /// <summary>
    /// Retrieves the content of a file based on the provided file identifier.
    /// </summary>
    /// <param name="id">The identifier of the file whose content needs to be retrieved.</param>
    /// <returns>The task result contains a <see cref="FileContentOutputDto"/> object with the file's content and metadata.</returns>
    Task<FileContentOutputDto> ExecuteAsync(int id);
}