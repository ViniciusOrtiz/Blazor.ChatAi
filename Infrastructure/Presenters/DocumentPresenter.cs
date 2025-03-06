using Application.Contracts.Presenters;
using Application.Models.Files;
using Domain.Entities;

namespace Infrastructure.Presenters;

public class DocumentPresenter : IDocumentPresenter
{
    public DocumentModel FromEntityToFileModel(DocumentEntity entity)
    {
        return new DocumentModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Size = entity.FileSizeInBytes,
            CreatedAt = entity.CreatedAt
        };
    }
}