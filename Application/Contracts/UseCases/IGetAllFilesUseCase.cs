using Application.Models.Files;

namespace Application.Contracts.UseCases;

public interface IGetAllFilesUseCase
{
    Task<IEnumerable<DocumentModel>> ExecuteAsync();
}