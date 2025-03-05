using Application.Contracts.Repositories.Common;
using Domain.Entities;

namespace Application.Contracts.Repositories
{
    public interface IDocumentRepository : IBaseRepository<DocumentEntity, int>
    {
        Task<ICollection<DocumentEntity>> QueryEmbeddingsAsync(float[] queryEmbedding);
    }
}
