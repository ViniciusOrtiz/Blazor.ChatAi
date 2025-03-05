using Application.Models.Files;
using Domain.Entities;

namespace Application.Contracts.Presenters;

public interface IDocumentPresenter
{
    DocumentModel FromEntityToFileModel(DocumentEntity entity);
}