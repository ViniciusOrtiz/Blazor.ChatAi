using Application.Contracts.Gateways;
using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Contracts.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using Application.Contracts.Presenters;
using Application.Models.Files;

namespace Application.UseCases;

public sealed class UploadDocumentUseCase : IUploadDocumentUseCase
{
    private readonly IFileService _fileService;
    private readonly IDocumentRepository _documentRepository;
    private readonly IAiGateway _aiGateway;
    private readonly IDocumentPresenter _documentPresenter;

    public UploadDocumentUseCase(
        IFileService fileService,
        IDocumentRepository documentRepository,
        IAiGateway aiGateway,
        IDocumentPresenter documentPresenter)
    {
        _fileService = fileService;
        _documentRepository = documentRepository;
        _aiGateway = aiGateway;
        _documentPresenter = documentPresenter;
    }

    /// <summary>
    /// Processes the upload of a document file, extracts its content, generates an embedding,
    /// creates a document entity, and returns the document model representation.
    /// </summary>
    /// <param name="file">The file to be uploaded and processed.</param>
    /// <returns>A <see cref="DocumentModel"/> containing metadata and details of the uploaded document.</returns>
    /// <exception cref="Exception">Thrown when the file content could not be read or processed successfully.</exception>
    public async Task<DocumentModel> ExecuteAsync(IBrowserFile file)
    {
        var fileName = file.Name;
        var fileSize = file.Size;

        await using var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        var fileContent = memoryStream.ToArray();

        var text = await _fileService.ExtractTextFromFileAsync(file);
        if (string.IsNullOrWhiteSpace(text))
            throw new Exception("The file could not be read. Try another file.");

        var embedding = await _aiGateway.GenerateEmbedding(text);
        var entity = new DocumentEntity(fileName, text, embedding, fileSize, fileContent);

        await _documentRepository.CreateAsync(entity);

        return _documentPresenter.FromEntityToFileModel(entity);
    }
}