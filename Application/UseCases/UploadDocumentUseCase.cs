using Application.Contracts.Gateways;
using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Contracts.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.UseCases;

public class UploadDocumentUseCase : IUploadDocumentUseCase
{
    private readonly IFileService _fileService;
    private readonly IDocumentRepository _documentRepository;
    private readonly IAiGateway _aiGateway;

    public UploadDocumentUseCase(IFileService fileService,
        IDocumentRepository documentRepository,
        IAiGateway aiGateway)
    {
        _fileService = fileService;
        _documentRepository = documentRepository;
        _aiGateway = aiGateway;
    }

    /// <summary>
    /// Executes the document upload process by extracting text from the provided file, generating embeddings,
    /// and storing the data in the repository.
    /// </summary>
    /// <param name="file">The file from which to extract text and generate embeddings.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ExecuteAsync(IBrowserFile file)
    {
        var text = await _fileService.ExtractTextFromFileAsync(file);
        if (string.IsNullOrWhiteSpace(text))
            return;

        var embedding = await _aiGateway.GenerateEmbedding(text);
        var entity = new DocumentEntity(text, embedding);

        await _documentRepository.CreateAsync(entity);
    }
}