using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Contracts.UseCases;
using Application.Models.Dtos.Files;

namespace Application.UseCases;

public class FileGetContentUseCase : IFileGetContentUseCase
{
    private readonly IDocumentRepository _documentRepository;
    private readonly ISecurityService _securityService;

    public FileGetContentUseCase(
        IDocumentRepository documentRepository,
        ISecurityService securityService)
    {
        _documentRepository = documentRepository;
        _securityService = securityService;
    }
    
    /// <summary>
    /// Retrieves the content of a file based on the provided file identifier.
    /// </summary>
    /// <param name="id">The identifier of the file whose content needs to be retrieved.</param>
    /// <returns>The task result contains a <see cref="FileContentOutputDto"/> object with the file's content and metadata.</returns>
    public async Task<FileContentOutputDto> ExecuteAsync(int id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        if (document is null)
        {
            throw new Exception("File not found");
        }

        return new FileContentOutputDto
        {
            Name = document.Name,
            Content = Convert.ToBase64String(_securityService.DecryptBytes(document.FileContent))
        };
    }
}