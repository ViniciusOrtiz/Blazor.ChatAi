using Application.Contracts.Presenters;
using Application.Contracts.Repositories;
using Application.Contracts.UseCases;
using Application.Models.Files;

namespace Application.UseCases;

public sealed class GetAllFilesUseCase : IGetAllFilesUseCase
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDocumentPresenter _documentPresenter;

    public GetAllFilesUseCase(
        IDocumentRepository documentRepository,
        IDocumentPresenter documentPresenter)
    {
        _documentRepository = documentRepository;
        _documentPresenter = documentPresenter;
    }
    
    public async Task<IEnumerable<DocumentModel>> ExecuteAsync()
    {
        var documents = await _documentRepository.GetAllAsync();
        return documents.Select(_documentPresenter.FromEntityToFileModel);
    }
}