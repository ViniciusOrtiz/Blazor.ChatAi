using Domain.Entities;

namespace Application.Contracts.Tools;

public interface IDocumentsTool
{
    Task<ICollection<DocumentEntity>> QueryDatabase(float[] questionEmbedding);
}