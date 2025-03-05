using Application.Contracts.Repositories;
using Application.Contracts.UseCases;

namespace Application.UseCases;

public class FileDeleteUseCase : IFileDeleteUseCase
{
    private readonly IDocumentRepository _documentRepository;

    public FileDeleteUseCase(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }
    
    public async Task ExecuteAsync(int id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        if (document is null)
        {
            throw new Exception("File not found");
        }
        
        await _documentRepository.DeleteAsync(document);
    }
}